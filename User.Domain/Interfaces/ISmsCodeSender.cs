using User.Domain.ValueObjects;

namespace User.Domain.Interfaces
{
    public interface ISmsCodeSender
    {
        Task SendAsync(PhoneNumber phoneNumber, string code);
    }
}
