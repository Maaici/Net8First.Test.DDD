
using System.Reflection;
using Zack.EventBus;

namespace _2_EventBusCustomer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<IntegrationEventRabbitMQOptions>(
                op => {
                    op.HostName = "192.168.20.209";
                    op.ExchangeName = "exchangeEventBusDemo1";
                    op.UserName = "guest1";
                    op.Password = "Aa000000";
                }
                );
            builder.Services.AddEventBus("queue2", Assembly.GetExecutingAssembly());

            var app = builder.Build();
            app.UseEventBus();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
