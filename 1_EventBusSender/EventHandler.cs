using Zack.EventBus;

namespace _1_EventBusSender
{
    [EventName("OrderCreated")]
    [EventName("OrderComplete")]
    public class EventHandler : JsonIntegrationEventHandler<dataModel>
    {
        public override Task HandleJson(string eventName, dataModel? eventData)
        {
            Console.WriteLine("消息生产者收到订单Json，eventData = " + eventData.name);
            return Task.CompletedTask;
        }
    }
    public class dataModel
    {
        public string name { get; set; }
        public int age { get; set; }
        public string tel { get; set; }
    }
}
