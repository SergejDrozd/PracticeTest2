namespace Domain.Events
{
    public record CategoryDeletedEvent(string CategoryName) : DomainEvent;
}
