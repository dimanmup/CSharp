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

            {
                FieldsWithRulesRequest req = new FieldsWithRulesRequest();
                req.Req = 1000;
                req.Opt = 100;

                FieldsWithRulesReply rep = client.FieldsWithRules(req);

                Console.WriteLine($"[{n++}]");
                Console.WriteLine(requestHeader);
                Console.WriteLine(JsonConvert.SerializeObject(req, Formatting.Indented));

                Console.WriteLine(replyHeader);
                Console.WriteLine(JsonConvert.SerializeObject(rep, Formatting.Indented));
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
