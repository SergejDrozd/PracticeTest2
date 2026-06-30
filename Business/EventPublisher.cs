using Domain.Events;
using Domain.Interfaces;

namespace Business
{
    public class EventPublisher : IEventPublisher
    {
        private readonly Dictionary<Type, List<Delegate>> handlers = new();

        public void Subscribe<T>(Action<T> handler) where T : DomainEvent
        {
            var eventType = typeof(T);
            if (!this.handlers.ContainsKey(eventType))
                this.handlers[eventType] = new List<Delegate>();

            this.handlers[eventType].Add(handler);
        }

        public void Publish<T>(T domainEvent) where T : DomainEvent
        {
            var eventType = domainEvent.GetType();
            if (!this.handlers.ContainsKey(eventType))
                return;

            foreach (var handler in this.handlers[eventType])
            {
                ((Action<T>)handler)(domainEvent);
            }
        }
    }
}
