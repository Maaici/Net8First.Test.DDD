using Zack.EventBus;

namespace _2_EventBusCustomer
{
    [EventName("OrderComplete")]
    public class EventHandler1 : IIntegrationEventHandler
    {
        Task IIntegrationEventHandler.Handle(string eventName, string eventData)
        {
            Console.WriteLine("消息消费者收到订单，eventData = " + eventData);
            return Task.CompletedTask;
        }
    }
}
