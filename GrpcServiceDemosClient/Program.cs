using Grpc.Net.Client;
using GrpcServiceDemos;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;

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
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
