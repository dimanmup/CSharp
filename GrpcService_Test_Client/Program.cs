using Grpc.Net.Client;
using GrpcService_Test.Services; // tests.proto: option csharp_namespace = "GrpcService_Test.Services";
using System;

namespace GrpcService_Test_Client
{
    class Program
    {
        static void Main()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            
            var client = new Hello.HelloClient(channel);

            HelloRequest request = new HelloRequest();

            Console.Write("request.Name: ");
            request.Name = Console.ReadLine();

            Console.Write("request.Age: ");
            request.Age = int.Parse(Console.ReadLine());

            HelloReply reply = client.SayHello(request);

            Console.WriteLine("reply.Message: " + reply.Message);
            Console.WriteLine("reply.Status: " + reply.Status);

            Console.ReadKey();
        }
    }
}
