namespace Domain.Events
{
    public record UserLoggedInEvent(string Username) : DomainEvent;
}
