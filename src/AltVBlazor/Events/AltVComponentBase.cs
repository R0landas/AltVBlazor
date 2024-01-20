using Microsoft.AspNetCore.Components;

namespace AltVBlazor.Events;

public abstract class AltVComponentBase : ComponentBase
{
    [Inject]
    public IAltVEventSubscriber Subscriber { get; set; } = null!;

    [Inject]
    public IAltVEventEmitter Emitter { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        await Subscriber.SubscribeEventsForComponent(this);
        await base.OnInitializedAsync();
    }

    protected ValueTask Emit(string eventName, params object[] args)
    {
        return Emitter.Emit(eventName, args);
    }
}