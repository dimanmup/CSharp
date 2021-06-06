using Confluent.Kafka;
using System;

namespace Kafka_1_Producing
{
    class Program
    {
        static void Main()
        {
            string server = "192.168.182.136:9092";
            
            ProducerConfig config = new ProducerConfig 
            {
                BootstrapServers = server
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();
            var topicMessage = new Message<Null, string>()
            {
                Value = "Hello World!" 
            };

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
                    producer.Produce("mytopic1", topicMessage);

                    Console.SetCursorPosition(10, 2);
                    Console.Write((++produced).ToString().PadRight(100, ' '));
                }
            }
            while (consoleKey != ConsoleKey.Escape);
        }
    }
}
