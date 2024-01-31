using User.Domain.Entities;
using User.Domain.ValueObjects;
using Zack.Commons;

namespace User.Domain
{
    public record UserModel : IAggregateRoot
    {
        public Guid Id { get; init; }

        public PhoneNumber PhoneNumber { get; set; }

        private string? PasswordHash { get; set; }

        public UserAccessFail UserAccessFail { get; private set; }

        private UserModel() { }
        private UserModel(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            Id = Guid.NewGuid();
            this.UserAccessFail = new UserAccessFail(this);
        }

        public bool HasPassword()
        {
            return !string.IsNullOrWhiteSpace(this.PasswordHash);
        }

        public void ChanegePassword(string password)
        {
            if (password.Length <= 3)
            {
                throw new ArgumentOutOfRangeException("password 长度必须大于3");
            }
            this.PasswordHash = HashHelper.ComputeMd5Hash(password);
        }

        public bool checkPassword(string password)
        {
            return this.PasswordHash == HashHelper.ComputeMd5Hash(password);
        }

        public void ChangePhoneNumber(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }
    }
}
