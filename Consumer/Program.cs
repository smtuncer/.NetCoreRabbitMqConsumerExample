using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

// connectionFactory --> Connection ---> Channel 
var connectionFactory = new ConnectionFactory()
{
    HostName = "localhost",
    //If you want to remote connection 
    //Uri = new Uri(""),
    //UserName = "",
    //Password = "",
};
var connection = connectionFactory.CreateConnection();
var channel = connection.CreateModel();

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (sender, args) =>
{
    var byteMessage = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(byteMessage);
    Console.WriteLine("Message: " + message);
};

channel.BasicConsume(queue: "Test", autoAck: true, consumer: consumer);

Console.WriteLine("..");
Console.ReadKey();