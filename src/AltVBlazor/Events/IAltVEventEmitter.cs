namespace AltVBlazor.Events;

public interface IAltVEventEmitter
{
    ValueTask Emit(string eventName, params object[] args);
}