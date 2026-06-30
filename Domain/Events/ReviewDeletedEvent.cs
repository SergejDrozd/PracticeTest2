namespace Domain.Events
{
    public record ReviewDeletedEvent(int ProductId) : DomainEvent;
}
