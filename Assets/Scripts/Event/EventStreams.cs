using SimpleEventBus;
using SimpleEventBus.Interfaces;

namespace Event
{
    public static class EventStreams
    {
        public static IEventBus UserInterface { get; } = new EventBus();
    }
}