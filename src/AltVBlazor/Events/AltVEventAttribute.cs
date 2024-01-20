namespace AltVBlazor.Events;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AltVEventAttribute(string eventName) : Attribute
{
    public string EventName { get; set; } = eventName;
}