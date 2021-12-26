using Grpc.Core;
using Grpc.Net.Client;
using GrpcServiceDemos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GrpcServiceDemosClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string requestHeader = "\nREQUEST\n-------";
            string replyHeader = "\nREPLY\n-----";
            int n = 1;

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Demos.DemosClient(channel);

            // Правила полей
            {
                FieldsWithRulesRequest req = new FieldsWithRulesRequest();
                req.Req = 1000;
                req.Opt = 100;
                req.Rep.Add(10);
                req.Rep.Add(1);

                FieldsWithRulesReply rep = client.FieldsWithRules(req);

                Console.WriteLine($"[{n++}]");
                Console.WriteLine(requestHeader);
                Console.WriteLine(JsonConvert.SerializeObject(req, Formatting.Indented));

                Console.WriteLine(replyHeader);
                Console.WriteLine(JsonConvert.SerializeObject(rep, Formatting.Indented));
                Console.WriteLine(new string('_', 100));
                Console.WriteLine();
            }

            // Перечисления
            {
                EnumsHandlerRequest req = new EnumsHandlerRequest();
                req.Gender = genders.Male;

                EnumsHandlerReply rep = client.EnumsHandler(req);

                Console.WriteLine($"[{n++}]");
                Console.WriteLine(requestHeader);
                Console.WriteLine($"I am a {req.Gender}!");

                Console.WriteLine(replyHeader);
                Console.WriteLine($"You are not a {rep.Gender}!");
                Console.WriteLine(new string('_', 100));
                Console.WriteLine();
            }

            // Stream
            {
                string source = @"E:\Docs\.NET\Троелсен (2018).pdf";
                string destination = $@"{Environment.GetEnvironmentVariable("UserProfile")}\Desktop\Троелсен (2018).pdf";

                Console.WriteLine($"[{n++}]");
                Console.WriteLine();
                Console.WriteLine($"Streaming \"{source}\" --> \"{destination}\"");
                Console.WriteLine();

                StreamGetterRequest req = new StreamGetterRequest();
                req.FileName = source;

                var rep = client.StreamGetter(req);
                var query = rep.ResponseStream.ReadAllAsync();

                StreamGetter_ResponseHandler(query, destination).Wait();

                Console.WriteLine(new string('_', 100));
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        static async Task StreamGetter_ResponseHandler(IAsyncEnumerable<StreamGetterReply> query, string destinationPath)
        {
            using (FileStream fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                long writtenBytes = 0;

                await foreach (var reply in query)
                {
                    writtenBytes += reply.FileBytes.Length;
                    fs.Write(reply.FileBytes.ToByteArray());

                    Console.Write($"\rHandled: {writtenBytes} bytes");

                    Thread.Sleep(100);
                }
            }

            Console.WriteLine();
        }
    }
}
