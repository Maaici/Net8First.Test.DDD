using User.Domain.Events;
using User.Domain.ValueObjects;

namespace User.Domain
{
    public interface IUserRepository
    {
        public Task<UserModel?> FindOneAsync(PhoneNumber phoneNumber);
        public Task<UserModel?> FindOneAsync(Guid userId);
        public Task AddNewLoginHistory(PhoneNumber phoneNumber, string message);
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code);
        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber);
        public Task PublishEventAsync(UserAccessResultEvent _event);
    }
}
