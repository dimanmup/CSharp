using Confluent.Kafka;
using System;

namespace Kafka_Produce
{
    class Program
    {
        static void Main()
        {
            string server = "192.168.182.137:9092";

            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = server
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();
            var topicMessage = new Message<Null, string>();

            Console.WriteLine("Enter - Produce");
            Console.WriteLine("Escape - Exit");
            Console.Write("Produced: ");

            int produced = 0;
            ConsoleKey consoleKey;
            do
            {
                consoleKey = Console.ReadKey().Key;
                if (consoleKey == ConsoleKey.Enter)
                {
                    topicMessage.Value = string.Concat("Hello World!", " ", DateTime.Now);

                    producer.Produce("mytopic1", topicMessage);

                    Console.SetCursorPosition(10, 2);
                    Console.WriteLine(new string(' ', 50));
                    Console.SetCursorPosition(10, 2);
                    Console.Write(++produced);
                }
            }
            while (consoleKey != ConsoleKey.Escape);
        }
    }
}
