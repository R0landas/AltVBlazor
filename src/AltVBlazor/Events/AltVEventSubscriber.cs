using System.Reflection;
using Microsoft.JSInterop;

namespace AltVBlazor.Events;

internal sealed class AltVEventSubscriber(IJSRuntime jsRuntime) : IAltVEventSubscriber
{
    private const string RegisterCallback = "registerCallback";
    
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);
    
    public async ValueTask SubscribeEventsForComponent<TComponent>(TComponent component) where TComponent : class
    {
        var eventHandlers = component
            .GetType()
            .GetMethods()
            .Where(mi => mi.GetCustomAttribute(typeof(AltVEventAttribute)) is not null);

        foreach (var eventHandler in eventHandlers)
        {
            var eventAttribute = eventHandler.GetCustomAttribute<AltVEventAttribute>()!;
            var objectRef = DotNetObjectReference.Create(component);
            await jsRuntime.InvokeVoidAsync(RegisterCallback, eventAttribute.EventName, objectRef, eventHandler.Name);
        }
    }
}