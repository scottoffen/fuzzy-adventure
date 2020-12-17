using System;
using System.Text;
using RabbitMQ.Client;

namespace Fuzzy.Producer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostname = "localhost";

            Console.WriteLine("********************************************************************************");
            Console.WriteLine("* Fuzzy Producer Started                                                       *");
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

                    string message = string.Empty;
                    while (true)
                    {
                        Console.WriteLine("Enter a message to send or [q] to quit.");
                        message = Console.ReadLine();
                        if (message == "q") break;

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body
                        );

                        Console.WriteLine($"[x] Sent: {message}");
                    }

                    Console.WriteLine("********************************************************************************");
                    Console.WriteLine("* Fuzzy Producer Stopped                                                       *");
                    Console.WriteLine("********************************************************************************");
                }
            }

            Console.ReadLine();
        }
    }
}
