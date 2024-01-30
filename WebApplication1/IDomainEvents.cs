using MediatR;

namespace WebApplication1
{
    public interface IDomainEvents
    {
        IEnumerable<INotification> GetDomainEvents();

        void AddDomainEvent(INotification notification);

        void ClearDomainEvents();
    }
}
