# AltV Blazor

A simple Blazor package to help you with event emitting and handling inside of WebViews written using Blazor.
The package is meant for Blazor WASM only, however it should also work on Blazor Server,
but you might encounter some unexpected behaviour.

## Installation
Add nuget package
```shell
dotnet add package AltVBlazor --version 0.2.0
```
## Setup
1. Register AltVBlazor services to your DI container in `program.cs`
   ```csharp
   builder.Services.AddAltVBlazor();
   ```
2. Add AltVBlazor server Javascript file to wwwroot/index.html
    ```html
    <script src="_content/AltVBlazor/AltVBlazorEvents.js"></script>
    ```
## Handling AltV events inside of a Blazor Component
You can either have your component inherit from `AltVComponentBase`, or inject `IAltVEventSubscriber`
and subscribe to events yourself

### Inherit from `AltVComponentBase` component
To register an event handler, create a method and add `JSInvokable` and `AltVEvent` attributes
events will be automatically subscribed.

⚠️ IMPORTANT:

if you override `OnInitializedAsync` make sure to call `base.OnInitializedAsync();' or events will not be subscribed.
```csharp
@inherits AltVComponentBase

@code {
   private bool chatInputVisible = false;
   private readonly List<ChatMessage> messages = [];
   
   [JSInvokable]
   [AltVEvent("addChatMessage")]
   public void OnChatMessageAdded(string json)
   {
     var chatMessage = ChatMessageExtensions.Deserialize(json);
     
     messages.Add(chatMessage);
     StateHasChanged();
   }
   
   [JSInvokable]
   [AltVEvent("toggleChat")]
   public void OnToggleChat(bool toggle)
   {
     chatInputVisible = toggle;
     StateHasChanged();
   }
}
```
### Inject `IAltVEventSubscriber`
Inject `IAltVEventSubscriber` and call `IAltVEventSubscriber.SubscribeEventsForComponent` when the component is initialized.
```csharp
@inject IAltVEventSubscriber AltVEventSubscriber

@code {
   private bool chatInputVisible = false;
   private readonly List<ChatMessage> messages = [];
    
   protected override async Task OnInitializedAsync()
   {
     await AltVEventSubscriber.SubscribeEventsForComponent(this);
     await base.OnInitializedAsync();
   }
   
   [JSInvokable]
   [AltVEvent("addChatMessage")]
   public void OnChatMessageAdded(string json)
   {
     var chatMessage = ChatMessageExtensions.Deserialize(json);
     
     messages.Add(chatMessage);
     StateHasChanged();
   }
   
   [JSInvokable]
   [AltVEvent("toggleChat")]
   public void OnToggleChat(bool toggle)
   {
     chatInputVisible = toggle;
     StateHasChanged();
   }
}
```
## Emitting events to client
if your component inherits from `AltVComponentBase`, use `Emit` method
```csharp
private async void OnSubmit(object? sender, string e)
{
    await Emit(ChatWebViewEvents.SubmitChatInput, e);
}
```
otherwise, inject `IAltVEventEmitter`
```csharp
@inject IAltVEventEmitter Emitter

@code {
    private async void OnSubmit(object? sender, string e)
    {
        await Emitter.Emit(ChatWebViewEvents.SubmitChatInput, e);
    }
}
```

## Debugging in browser
If you open Blazor APP in a browser, all emit events will be logged to the console.
In addition to that, you can trigger callbacks using
```javascript
window.dispatchEvent(new CustomEvent('eventName', {detail: argArray}));
```




