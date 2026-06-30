using Domain.Enums;

namespace Domain.Events
{
    public record UserRoleChangedEvent(string Username, Role OldRole, Role NewRole) : DomainEvent;
}
