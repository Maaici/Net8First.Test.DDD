using Zack.EventBus;

namespace _2_EventBusCustomer
{
    [EventName("OrderCreated")]
    public class EventHandler : IIntegrationEventHandler
    {
        Task IIntegrationEventHandler.Handle(string eventName, string eventData)
        {
            Console.WriteLine("消息消费者收到订单，eventData = " + eventData);
            return Task.CompletedTask;
        }
    }
}
