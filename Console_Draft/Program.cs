using DotNetTor.SocksPort;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;



namespace Console_Draft
{
    class Program
    {
        static void Main()
        {
            var requestUri = "http://icanhazip.com/";
            using (var httpClient = new HttpClient())
            {
                var message = httpClient.GetAsync(requestUri).Result;
                var content = message.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Your real IP: \t\t{content}");
            }
            
            // 2. Get Tor IP
            using (var httpClient = new HttpClient(new SocksPortHandler("127.0.0.1", socksPort: 9050)))
            {
                var message = httpClient.GetAsync(requestUri).Result;
                var content = message.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Your Tor IP: \t\t{content}");

                // 3. Change Tor IP
                var controlPortClient = new DotNetTor.ControlPort.Client("127.0.0.1", controlPort: 9051, password: "ILoveBitcoin21");
                controlPortClient.ChangeCircuitAsync().Wait();

                // 4. Get changed Tor IP
                message = httpClient.GetAsync(requestUri).Result;
                content = message.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Your other Tor IP: \t{content}");
            }
            Console.ReadKey();


            //string file = "x.pdf";
            //string zip = "x.zip";

            //using (FileStream fsW = new FileStream(zip, FileMode.Open, FileAccess.Write))
            //using (ZipOutputStream zo = new ZipOutputStream(fsW))
            //using (FileStream fsR = new FileStream(file, FileMode.Open, FileAccess.Read))
            //{
            //    zo.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
            //    zo.PutNextEntry(file);
            //    fsR.CopyTo(zo);
            //}
        }
    }
}
