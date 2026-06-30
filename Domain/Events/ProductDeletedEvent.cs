namespace Domain.Events
{
    public record ProductDeletedEvent(string ProductName) : DomainEvent;
}
