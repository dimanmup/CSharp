using Confluent.Kafka;
using System;

namespace Kafka_ProduceWithHandler
{
    class Program
    {
        public static void handler(DeliveryReport<Null, string> report)
        {
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(new string(' ', 50));
            Console.SetCursorPosition(0, 2);
            Console.Write($"report.TopicPartitionOffsetError: \"{report.TopicPartitionOffsetError}\"");
        }

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

            ConsoleKey consoleKey;
            do
            {
                consoleKey = Console.ReadKey().Key;
                if (consoleKey == ConsoleKey.Enter)
                {
                    topicMessage.Value = string.Concat("Hello World!", " ", DateTime.Now);

                    producer.Produce("mytopic1", topicMessage, handler);
                }
            }
            while (consoleKey != ConsoleKey.Escape);

            // Enter - Produce
            // Escape - Exit
            // report.TopicPartitionOffsetError: "mytopic1 [[0]] @3: Success"
        }
    }
}
