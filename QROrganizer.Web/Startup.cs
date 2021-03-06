using QROrganizer.Data;
using IntelliTect.Coalesce;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QROrganizer.Data.Models;
using QROrganizer.Data.Policies;
using QROrganizer.Data.Services.Implementation;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Util;
using QROrganizer.Web.Util;
using CacheControlHeaderValue = Microsoft.Net.Http.Headers.CacheControlHeaderValue;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace QROrganizer.Web;

public class Startup
{
    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
        Configuration = configuration;
        Env = env;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:InstrumentationKey"]);
        services.AddSingleton<ITelemetryInitializer, TelemetryEnrichment>();

        const string connectionName = "DefaultConnection";
        string connString = Configuration.GetConnectionString(connectionName);

        // Add Entity Framework services to the services
        services.AddSingleton(Configuration);
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connString));

        services.AddOptions();
        services.Configure<AppConfigSettings>(Configuration.GetSection("AppConfigSettings"));

        services.AddCoalesce<AppDbContext>();

        services
            .AddMvc(options => options.EnableEndpointRouting = false)
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

        services.AddSwaggerGen();

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequiredUniqueChars = 1,
                    RequireNonAlphanumeric = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireDigit = true
                };

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "auth_cookie";
            options.Cookie.SameSite = SameSiteMode.None;
            options.LoginPath = new PathString("/api/contests");
            options.AccessDeniedPath = new PathString("/api/contests");
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

            // Not creating a new object since ASP.NET Identity has created
            // one already and hooked to the OnValidatePrincipal event.
            // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return Task.CompletedTask;
            };
        });

        // Add policy for each
        services.AddAuthorization(options =>
        {
            var features = Enum.GetValues<Feature>();
            foreach (var feature in features)
            {
                options.AddPolicy(
                    feature.ToString(),
                    policy => policy.Requirements.Add(new SubscriptionFeatureRequirement(feature)));
            }
        });
        services.AddSingleton<IAuthorizationHandler, SubscriptionFeaturePolicy>();

        // Build SendGridTemplateIds
        var sendGridTemplateIds = Configuration.GetSection("SendGridTemplateIds");
        var properties = typeof(SendGridTemplateIds).GetProperties();
        sendGridTemplateIds.AsEnumerable().ToList().ForEach(x =>
        {
            var index = x.Key.IndexOf(':');
            if (index < 1) return;
            var key = x.Key[(index+1)..];
            properties.Single(xi => xi.Name == key).SetValue(null, x.Value);
        });

        services.AddHttpClient<IHcaptchaHttpClient, HcaptchaHttpClient>(c =>
        {
            c.BaseAddress = new Uri("https://hcaptcha.com", UriKind.Absolute);
        });

        services.AddHttpClient<IBarcodeSpiderHttpClient, BarcodeSpiderHttpClient>(c =>
        {
            c.BaseAddress = new Uri("https://api.barcodespider.com/v1/", UriKind.Absolute);
            c.DefaultRequestHeaders.Add("token", Configuration["AppConfigSettings:BarcodeSpiderApiKey"]);
        });
        services.AddRateLimitingService<IBarcodeSpiderHttpClient>(TimeSpan.FromSeconds(2));

        services.AddAuthentication();

        services.AddScoped<UserInfoService>();
        services.AddScoped<IAccessCodeService, AccessCodeService>();
        services.AddScoped<ISiteInfoService, SiteInfoService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddSingleton<IUpcLookupService, UpcLookupService>();
        services.AddSingleton<HttpContextInfo>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

#pragma warning disable CS0618 // Type or member is obsolete
            app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
            {
                HotModuleReplacement = true,
                // Use a slightly-tweaked version of vue-cli's webpack config to work around a few bugs.
                ConfigFile = "webpack.config.aspnetcore-hmr.js",
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });

        // Routing
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        var containsFileHashRegex = new Regex(@"\.[0-9a-fA-F]{8}\.[^\.]*$", RegexOptions.Compiled);
        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = ctx =>
            {
                // vue-cli puts 8-hex-char hashes before the file extension.
                // Use this to determine if we can send a long-term cache duration.
                if (containsFileHashRegex.IsMatch(ctx.File.Name))
                {
                    ctx.Context.Response.GetTypedHeaders().CacheControl =
                        new CacheControlHeaderValue { Public = true, MaxAge = TimeSpan.FromDays(30) };
                }
            }
        });

        // For all requests that aren't to static files, disallow caching.
        app.Use(async (context, next) =>
        {
            context.Response.GetTypedHeaders().CacheControl =
                new CacheControlHeaderValue { NoCache = true, NoStore = true, };

            await next();
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            // API fallback to prevent serving SPA fallback to 404 hits on API endpoints.
            endpoints.Map("api/{**any}", ctx => { ctx.Response.StatusCode = StatusCodes.Status404NotFound; return Task.CompletedTask; });

            endpoints.MapFallbackToController("Index", "Home");
        });
    }
}
