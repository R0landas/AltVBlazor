namespace AltVBlazor.Events;

public interface IAltVEventSubscriber
{
    ValueTask SubscribeEventsForComponent<TComponent>(TComponent component) where TComponent : class;
}