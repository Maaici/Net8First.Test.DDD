using User.Domain.enums;
using User.Domain.Interfaces;
using User.Domain.ValueObjects;

namespace User.Domain
{
    public class UserDomainService
    {
        private readonly IUserRepository userRepository;
        private readonly ISmsCodeSender smsCodeSender;

        public UserDomainService(IUserRepository userRepository, ISmsCodeSender smsCodeSender)
        {
            this.userRepository = userRepository;
            this.smsCodeSender = smsCodeSender;
        }

        public void ResetAccessFail(UserModel user)
        {
            user.UserAccessFail.Reset();
        }

        public bool IsLockout(UserModel user)
        {
            return user.UserAccessFail.IsLockOut();
        }

        public void AccessFail(UserModel user)
        {
            user.UserAccessFail.Fail();
        }

        public async Task<UserAccessResult> CheckPssword(PhoneNumber phoneNumber, string password)
        {
            UserAccessResult result;
            var user = await userRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                result = UserAccessResult.PhoneNumberNotFound;
            }
            else if (IsLockout(user))
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.HasPassword() == false)
            {
                result = UserAccessResult.Lockout;
            }
            else if (user.checkPassword(password))
            {
                result = UserAccessResult.OK;
            }
            else
            {
                result = UserAccessResult.PasswordError;
            }
            if (user != null)
            {
                if (result == UserAccessResult.OK)
                {
                    ResetAccessFail(user);
                }
                else
                {
                    AccessFail(user);
                }
            }
            await userRepository.PublishEventAsync(new Events.UserAccessResultEvent(phoneNumber, result));
            return result;
        }

        public async Task<CheckCodeResult> CheckPhoneNumberCodeAsync(PhoneNumber phoneNumber, string code) { 
            UserModel? user =await userRepository.FindOneAsync(phoneNumber);
            if (user == null)
            {
                return CheckCodeResult.PhoneNumberNotFound;
            }
            else if (IsLockout(user)) { 
                return CheckCodeResult.Lockout;
            }
            string? serverCode = await userRepository.FindPhoneNumberCodeAsync(phoneNumber);
            if (serverCode == null)
            {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
            if (code == serverCode)
            {
                return CheckCodeResult.OK;
            }
            else {
                AccessFail(user);
                return CheckCodeResult.CodeError;
            }
        }
    }
}
