using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QROrganizer.Data;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Interface;
using QROrganizer.Data.Services.Models;
using QROrganizer.Data.Util;

namespace QROrganizer.Web.Api
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RestrictedAccessCode { get; set; }
    }

    [Route("api")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppConfigSettings _appConfigSettings;
        private readonly IAccessCodeService _accessCodeService;
        private readonly IEmailService _emailService;
        private readonly IHcaptchaHttpClient _hcaptchaHttpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<AppConfigSettings> appConfigSettings,
            IAccessCodeService accessCodeService,
            IEmailService emailService,
            IHcaptchaHttpClient hcaptchaHttpClient,
            IWebHostEnvironment webHostEnvironment,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _appConfigSettings = appConfigSettings?.Value ?? throw new ArgumentNullException(nameof(appConfigSettings));
            _accessCodeService = accessCodeService ?? throw new ArgumentNullException(nameof(accessCodeService));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _hcaptchaHttpClient = hcaptchaHttpClient ?? throw new ArgumentNullException(nameof(hcaptchaHttpClient));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials creds)
        {
            var user = await _userManager.FindByEmailAsync(creds.Email);

            if (user is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<string>{"Account with that email could not be found"}
                });
            }

            var res = await _signInManager.PasswordSignInAsync(
                user,
                creds.Password,
                false,
                false);

            if (res is null || !res.Succeeded)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Errors = new List<string>{"Email is not verified - click link in email"}
                    });
                }

                return new UnauthorizedResult();
            }

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            var loggedInUser = new UserInfo(claimsPrincipal);

            return Ok(loggedInUser);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LoginCredentials creds)
        {
            if (!_webHostEnvironment.IsDevelopment())
            {
                var hCaptchaToken = HttpContext.Request.Headers["h-captcha-response"];
                var hCaptchaVerifyResponse = await _hcaptchaHttpClient.VerifyCaptcha(hCaptchaToken);

                if (!hCaptchaVerifyResponse.Success)
                {
                    return BadRequest("Captcha verification failed. Refresh and try again");
                }
            }
            else
            {
                _logger.LogWarning("In Development, skipping HCaptcha verification!");
            }

            if (_appConfigSettings.RestrictedEnvironment)
            {
                var error = _accessCodeService
                    .CheckAccessCodeValidAndRetrieve(creds.RestrictedAccessCode, out _);
                if (error is not null)
                {
                    return BadRequest(error);
                }
            }

            if (creds.Username.Length > 16)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<string> { "Username must be 16 characters or less" }
                });
            }

            if (creds.Password != creds.ConfirmPassword) return new UnauthorizedResult();

            var user = new ApplicationUser
            {
                Email = creds.Email,
                UserName = creds.Username
            };

            var res = await _userManager.CreateAsync(user, creds.Password);

            if (!res.Succeeded)
            {
                return new UnauthorizedObjectResult(res);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var emailSuccess = await _emailService.SendConfirmationEmail(token, user.Id, user.Email);

            if (emailSuccess)
            {
                if (_appConfigSettings.RestrictedEnvironment)
                {
                    await _accessCodeService.ValidateAndUseAccessCode(creds.RestrictedAccessCode);
                }
                return new OkResult();
            }

            // If no email was sent successfully delete user so they can retry later
            await _userManager.DeleteAsync(user);
            return BadRequest("An error occurred. Please try again later");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
