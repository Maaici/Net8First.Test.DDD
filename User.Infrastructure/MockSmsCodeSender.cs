using User.Domain.Interfaces;
using User.Domain.ValueObjects;

namespace User.Infrastructure
{
    public class MockSmsCodeSender : ISmsCodeSender
    {
        public Task SendAsync(PhoneNumber phoneNumber, string code)
        {
            Console.WriteLine($"[XXX公司]短信验证码为:{code},5分钟内有效");
            return Task.CompletedTask;
        }
    }
}
