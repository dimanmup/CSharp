using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kafka_ProduceToPartition
{
    class Program
    {
        static int reportCount = 0;
        static int messageCount = 10_000;
        static List<long> partitions = new List<long>();

        static void handler(DeliveryReport<int, string> report)
        {
            if (++reportCount == messageCount)
            {
                Console.WriteLine("Done!");
            }

            if (!partitions.Any(p => p == report.Partition.Value))
            {
                partitions.Add(report.Partition.Value);
            }

            if (partitions.Count > 1) // reportCount > 9999
            {
                throw new Exception("The key does not work!");
            }
        }

        static void Main()
        {
            string server = "192.168.182.137:9092";
            string topic = "mytopic1";

            var partitionMessage = new Message<int, string>
            {
                Key = 12345,
                Value = "Lorem Ipsum 12345!"
            };

            ProducerConfig producerConfig = new ProducerConfig
            {
                BootstrapServers = server,
                Acks = Acks.Leader
            };
            var producer = new ProducerBuilder<int, string>(producerConfig).Build();

            Console.WriteLine("Producing...");

            for (int i = 0; i < messageCount; i++)
            {
                producer.Produce(topic, partitionMessage, handler);
            }

            Console.ReadKey();
        }
    }
}
