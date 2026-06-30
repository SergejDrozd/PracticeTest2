namespace Domain.Events
{
    public record ReviewAddedEvent(int ProductId, int Rating) : DomainEvent;
}
