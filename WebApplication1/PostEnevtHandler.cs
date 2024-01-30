using MediatR;

namespace WebApplication1
{
    public class PostEnevtHandler : NotificationHandler<PostNotification>
    {
        protected override void Handle(PostNotification notification)
        {
            Console.WriteLine($"监听到事件被触发：{DateTime.Now}");
        }
    }
}
