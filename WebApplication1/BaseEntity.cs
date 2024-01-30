using MediatR;

namespace WebApplication1
{
    public abstract class BaseEntity : IDomainEvents
    {
        private readonly IList<INotification> events = new List<INotification>();
        public void AddDomainEvent(INotification notification)
        {
            events.Add(notification);
        }

        public void ClearDomainEvents()
        {
            events.Clear();
        }

        public IEnumerable<INotification> GetDomainEvents()
        {
            return events;
        }
    }
}
