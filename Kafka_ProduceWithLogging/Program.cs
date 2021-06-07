using Confluent.Kafka;
using System;
using System.Threading;

namespace Kafka_ProduceWithLogging
{
    class Program
    {
        static bool producerIsOpen = false;

        static void ProducerLogger(IProducer<Null, string> sender, LogMessage log)
        {
            if (log.Level == SyslogLevel.Error)
            {
                Console.WriteLine();
                Console.WriteLine(log.Message);

                producerIsOpen = false;

                sender.Dispose();
            }
        }

        static void Main()
        {
            string server = "192.168.182.136:9092";

            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = server
            };

            var producer = new ProducerBuilder<Null, string>(config)
                .SetLogHandler(ProducerLogger)
                .Build();

            producerIsOpen = true;
            int dotX = 0;

            while (producerIsOpen)
            {
                if (dotX++ < 3)
                {
                    Console.Write('.');
                    Thread.Sleep(500);
                    continue;
                }

                dotX = 0;
                Console.Clear();
                Console.SetCursorPosition(0, 0);
            }

            Console.WriteLine("Producer has been disposed.");
            Console.ReadKey();
        }
    }
}
