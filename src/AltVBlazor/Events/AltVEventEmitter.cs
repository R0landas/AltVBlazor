using Microsoft.JSInterop;

namespace AltVBlazor.Events;

internal sealed class AltVEventEmitter(IJSRuntime jsRuntime) : IAltVEventEmitter
{
    private const string EmitClientEvent = "emitClient";
    
    public ValueTask Emit(string eventName, params object[] args)
    {
        return jsRuntime.InvokeVoidAsync(EmitClientEvent, eventName, args);
    }
}