using System;
using Microsoft.Extensions.DependencyInjection;
using QROrganizer.Data.Services.Implementation;
using QROrganizer.Data.Services.Interface;

namespace QROrganizer.Web.Util;

// ReSharper disable once InconsistentNaming
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddRateLimitingService<T>(
        this IServiceCollection serviceCollection,
        TimeSpan rateLimit)
        where T : class
    {
        serviceCollection.AddSingleton<IRateLimitingService<T>>(_ => new RateLimitingService<T>(rateLimit));
        return serviceCollection;
    }
}
