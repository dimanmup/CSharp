using Confluent.Kafka;
using System;

namespace Kafka_ConsumerAllPartitions
{
    class Program
    {
        static void Main()
        {
            string server = "192.168.182.137:9092";

            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = server,
                GroupId = DateTime.Now.ToString(), // EnableAutoCommit == true (defaul)
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("mytopic1");

            DateTime previousMessageDateTime = new DateTime(0);
            bool sortedByTimestamp = true;
            for (int i = 0; i < 30; i++)
            {
                var consumeResult = consumer.Consume();

                if (sortedByTimestamp)
                {
                    if (consumeResult.Message.Timestamp.UtcDateTime < previousMessageDateTime)
                    {
                        sortedByTimestamp = false;
                    }
                    else
                    {
                        previousMessageDateTime = consumeResult.Message.Timestamp.UtcDateTime;
                    }
                }

                Console.WriteLine($"TopicPartitionOffset: {consumeResult.TopicPartitionOffset}");
                Console.WriteLine($"Key: {consumeResult.Message.Key}");
                Console.WriteLine($"Value: {consumeResult.Message.Value}");
                Console.WriteLine($"Timestamp: {consumeResult.Message.Timestamp.UtcDateTime}");
                Console.WriteLine();
            }

            consumer.Close();

            Console.WriteLine($"sortedByTimestamp: {sortedByTimestamp}");
            Console.WriteLine("Consumer has been closed.");
            Console.ReadKey();

            //TopicPartitionOffset: mytopic1 [[1]] @0
            //Key:
            //Value: test[1][15.06.2021 12:24:38]
            //Timestamp: 15.06.2021 9:24:38
            // ...

            //sortedByTimestamp: False
            //Consumer has been closed.
        }
    }
}
