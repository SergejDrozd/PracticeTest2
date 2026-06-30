namespace Domain.Events
{
    public record PriceChangedEvent(string ProductName, decimal OldPrice, decimal NewPrice) : DomainEvent;
}
