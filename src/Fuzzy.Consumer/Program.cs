using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Fuzzy.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostname = "localhost";

            Console.WriteLine("********************************************************************************");
            Console.WriteLine("* Fuzzy Consumer Started                                                       *");
            Console.WriteLine("********************************************************************************");

            Console.Write("Creating connection factory...");
            var factory = new ConnectionFactory{ HostName = hostname };
            Console.WriteLine("done");

            Console.Write($"Creating connection to {hostname}...");
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("done");

                Console.Write("Creating channel...");
                using (var channel = connection.CreateModel())
                {
                    Console.WriteLine("done");
                    Console.WriteLine();

                    channel.QueueDeclare(
                        queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments:null
                    );

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"[x] Received: {message}");
                    };

                    channel.BasicConsume(
                        queue: "hello",
                        autoAck: true,
                        consumer: consumer
                    );

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();

                    Console.WriteLine("********************************************************************************");
                    Console.WriteLine("* Fuzzy Consumer Stopped                                                       *");
                    Console.WriteLine("********************************************************************************");
                }
            }
        }
    }
}
