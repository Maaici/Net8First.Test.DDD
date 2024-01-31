namespace User.Domain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }

        public UserModel User { get; init; }

        public Guid UserId { get; init; }

        private bool isLockOut;

        public DateTime? LockEnd { get; private set; }

        public int AccessCount { get; private set; }

        private UserAccessFail() { }

        public UserAccessFail(UserModel user)
        {
            this.User = user;
            this.Id = Guid.NewGuid();
        }

        public void Reset()
        {
            this.AccessCount = 0;
            this.isLockOut = false;
            this.LockEnd = null;
        }

        public void Fail()
        {
            this.AccessCount++;
            if (this.AccessCount >= 3)
            {
                this.LockEnd = DateTime.Now.AddMinutes(5);
                this.isLockOut = true;
            }
        }

        public bool IsLockOut()
        {
            if (this.isLockOut)
            {
                if (DateTime.Now > this.LockEnd) // 已过期
                {
                    Reset();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
