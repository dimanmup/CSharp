using Grpc.Net.Client;
using GrpcService_Demos.Services;
using Newtonsoft.Json;
using System;

namespace GrpcService_Demos_Client
{
    class Program
    {
        static void Main()
        {
            int n = 1;
            string requestHeader = "-- Request --";
            string replyHeader = "-- Reply --";

            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Demos.DemosClient(channel);

            #region FieldsRulesHandler

            {
                FieldsWithRulesRequest req1 = new FieldsWithRulesRequest();

                req1.Req = 1000;
                //req1.Opt = 100;
                //req1.Rep.Add(10);
                //req1.Rep.Add(1);

                Console.WriteLine($"[{n++}]");
                Console.WriteLine(requestHeader);
                Console.WriteLine(JsonConvert.SerializeObject(req1, Formatting.Indented));

                FieldsWithRulesReply rep1 = client.FieldsRulesHandler(req1);

                Console.WriteLine(replyHeader);
                Console.WriteLine(JsonConvert.SerializeObject(rep1, Formatting.Indented));
            }

            #endregion FieldsRulesHandler

            Console.ReadKey();
        }
    }
}
