using RabbitMQ.Client;
using System.Text;

namespace MediatRDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var factory = new ConnectionFactory();
            factory.UserName ="guest1"; //用户名
            factory.Password = "Aa000000"; //密码
            factory.HostName = "192.168.20.209"; //服务器地址
            //factory.Port = 15672;
            factory.DispatchConsumersAsync = true;
            string exchangeName = "exchange1"; // 交换机的名字
            string eventName = "myEvent";//routingKey的值
            using var conn = factory.CreateConnection();

            int i = 0;
            while (i < 1000)
            {
                string msg = DateTime.Now.ToString();
                using (var channel = conn.CreateModel())//创建信道
                { 
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    channel.ExchangeDeclare(exchangeName, type:"direct"); //声明交换机
                    byte[] body = Encoding.UTF8.GetBytes(msg);
                    channel.BasicPublish(exchangeName, eventName, true, properties, body); //发布消息
                }
                Console.WriteLine("发送了一个消息：" + msg);
                i++;
                Thread.Sleep(1000);
            }

        }
    }
}
