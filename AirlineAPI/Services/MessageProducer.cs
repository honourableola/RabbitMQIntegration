using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace AirlineAPI.Services;

public class MesssageProducer : IMessageProducer
{
    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password",
            VirtualHost = "/"
        };

        var conn = factory.CreateConnection();

        using var channel = conn.CreateModel();

        channel.QueueDeclare("bookings", durable: true, exclusive: true);
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        channel.BasicPublish("", "Bookings", body: body);
    }
}