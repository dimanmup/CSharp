using Confluent.Kafka;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Kafka_MyProducer_ErrorHandling
{
    class MyProducer : IDisposable
    {
        private readonly ProducerConfig config;
        private readonly Message<Null, string> message = new Message<Null, string>();
        private IProducer<Null, string> producer;
        private bool disposed = false;

        public readonly string ServerIPAddress;
        public readonly int ServerPort;

        public void Dispose()
        {
            Console.WriteLine("Producer has been disposed.");

            disposed = true;
            producer?.Dispose();
            producer = null;
        }

        public MyProducer(string serverIPAddress = "localhost", int serverPort = 9092)
        {
            config = new ProducerConfig
            {
                BootstrapServers = string.Join(":", serverIPAddress, serverPort),
                LogConnectionClose = false
            };

            ServerIPAddress = serverIPAddress;
            ServerPort = serverPort;
        }

        private void handler(DeliveryReport<Null, string> report)
        {
            Console.WriteLine($"report.Value: \"{report.Value}\"");
            Console.WriteLine($"report.TopicPartitionOffsetError: \"{report.TopicPartitionOffsetError}\"");
            Console.WriteLine($"Status: {report.Error.Code}");
            Console.WriteLine();
        }

        private void ProducerErrorHandler(IProducer<Null, string> sender, Error e)
        {
            if (e.IsError)
            {
                Console.WriteLine($"Error.Code: {e.Code}");
                Console.WriteLine($"Error.Reason: {e.Reason}");
                Console.WriteLine();

                disposed = true;
            }
        }

        public void Produce(
            string topic,
            string messageText,
            TimeSpan interval,
            int count = 1,
            bool withCounter = false,
            bool withDataTime = false)
        {
            producer = new ProducerBuilder<Null, string>(config)
                .SetErrorHandler(ProducerErrorHandler)
                .Build();

            try
            {
                using (var client = new TcpClient(ServerIPAddress, ServerPort))
                {
                    int i = 1;
                    while (!disposed && client.Connected && i <= count)
                    {
                        message.Value = messageText;

                        if (withCounter) message.Value = string.Concat(message.Value, " [", i, "]");
                        if (withDataTime) message.Value = string.Concat(message.Value, " [", DateTime.Now, "]");

                        producer.Produce(topic, message, handler);

                        if (i == count)
                        {
                            producer.Flush();
                            Console.WriteLine("All messages were produced.");
                            break;
                        }

                        Thread.Sleep(interval);
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Dispose();
            }
        }
    }

    class Program
    {
        static void Main()
        {
            MyProducer mp = new MyProducer("192.168.182.137");
            mp.Produce("mytopic1", "test", TimeSpan.FromSeconds(1), 50, true, true);

            Console.ReadKey();

            //report.Value: "test [1] [09.06.2021 12:52:33]"
            //report.TopicPartitionOffsetError: "mytopic1 [[0]] @145: Success"
            //Status: NoError

            //report.Value: "test [2] [09.06.2021 12:52:34]"
            //report.TopicPartitionOffsetError: "mytopic1 [[0]] @146: Success"
            //Status: NoError

            //report.Value: "test [3] [09.06.2021 12:52:35]"
            //report.TopicPartitionOffsetError: "mytopic1 [[0]] @147: Success"
            //Status: NoError

            //report.Value: "test [4] [09.06.2021 12:52:36]"
            //report.TopicPartitionOffsetError: "mytopic1 [[0]] @148: Success"
            //Status: NoError

            //report.Value: "test [5] [09.06.2021 12:52:37]"
            //report.TopicPartitionOffsetError: "mytopic1 [[0]] @149: Success"
            //Status: NoError

            //All messages were produced.
            //Producer has been disposed.
        }
    }
}
