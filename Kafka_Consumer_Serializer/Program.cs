using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace Kafka_Consumer_Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = "192.168.182.137:9092",
                GroupId = DateTime.Now.ToString(),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true
            };

            var consumer = new ConsumerBuilder<Null, Car>(config)
                .SetValueDeserializer(new CarDeserializer())
                .Build();

            consumer.Subscribe("mytopic1");

            int n = 0;
            while (true)
            {
                var consumeResult = consumer.Consume();

                Console.Clear();
                Console.WriteLine(++n);

                if (consumeResult.IsPartitionEOF)
                {
                    Console.WriteLine("All messages have been consumed, new data awaiting...");
                }
                else if (consumeResult?.Message?.Value != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(consumeResult.Message, Formatting.Indented));
                }
                else
                {
                    Console.WriteLine("Skipping different messages.");
                }

                Thread.Sleep(10);
            }
        }
    }
}
