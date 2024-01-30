using MediatR;

namespace WebApplication1
{
    public record PostNotification(string body) : INotification;
}
