using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace RabbitMqCustomer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var factory = new ConnectionFactory();
            factory.UserName = "guest1"; //用户名
            factory.Password = "Aa000000"; //密码
            factory.HostName = "192.168.20.209"; //服务器地址
            factory.DispatchConsumersAsync = true;

            string exchangeName = "exchange1"; // 交换机的名字
            string eventName = "myEvent";//routingKey的值
            using var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();
            string queueName = "queue1";
            channel.ExchangeDeclare(exchangeName,"direct");
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName,exchangeName,eventName);

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Customer_Received;
            channel.BasicConsume(queueName, false, consumer);
            Console.ReadLine();

            async Task Customer_Received(object sender, BasicDeliverEventArgs @event)
            {
                try
                {
                    var bytes = @event.Body.ToArray();
                    string msg = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(DateTime.Now + "收到了信息：" + msg);
                    //DeliveryTag为消息的编号
                    channel.BasicAck(@event.DeliveryTag, false);
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    channel.BasicReject(@event.DeliveryTag, true);
                    await Console.Out.WriteLineAsync($"接收消息是发生错误：{ex.Message}");
                }
            }
        }

        
    }
}
