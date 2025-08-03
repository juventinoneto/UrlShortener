using Microsoft.Extensions.DependencyInjection;
using UrlShortener_AppServices.Interfaces;
using UrlShortener_AppServices.Services;

namespace UrlShortenerDI;

public static class Bootstrap
{
    public static IServiceCollection ResolveServices(this IServiceCollection services)
    {
        services.AddScoped<IUrlAppService, UrlAppService>();

        return services;
    }
}
