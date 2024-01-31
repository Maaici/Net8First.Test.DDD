using MediatR;
using User.Domain.enums;
using User.Domain.ValueObjects;

namespace User.Domain.Events
{
    public record UserAccessResultEvent(PhoneNumber PhoneNumber, UserAccessResult Result) : INotification;
}
