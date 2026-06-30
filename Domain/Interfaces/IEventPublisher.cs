using Domain.Events;

namespace Domain.Interfaces
{
    public interface IEventPublisher
    {
        void Subscribe<T>(Action<T> handler) where T : DomainEvent;
        void Publish<T>(T domainEvent) where T : DomainEvent;
    }
}
