using AltVBlazor.Events;
using Microsoft.Extensions.DependencyInjection;

namespace AltVBlazor.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAltVBlazor(this IServiceCollection services)
    {
        services.AddSingleton<IAltVEventSubscriber, AltVEventSubscriber>();
        services.AddSingleton<IAltVEventEmitter, AltVEventEmitter>();
        
        return services;
    }
}