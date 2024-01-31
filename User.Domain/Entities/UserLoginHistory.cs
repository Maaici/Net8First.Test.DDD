using User.Domain.ValueObjects;

namespace User.Domain.Entities
{
    public class UserLoginHistory : IAggregateRoot
    {
        public Guid Id { get; init; }

        public Guid? UserId { get; init; }


        public PhoneNumber PhoneNumber { get; init; }


        public DateTime CreateDateTime { get; init; }


        public string Message { get; init; }

        private UserLoginHistory() { }

        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, string message)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId;
            this.PhoneNumber = phoneNumber;
            this.Message = message;
        }



    }
}
