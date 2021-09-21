using System;
using System.IO;
using System.Threading;

namespace Stream_StreamReader_2Threads
{
    class Program
    {
        static Random rnd = new Random();
        static string path = "AZ.txt";

        static void Main()
        {
            CreateTestFile(path);

            Thread reading1 = new Thread(Readind);
            Thread reading2 = new Thread(Readind);

            reading1.Start();
            reading2.Start();

            reading1.Join();
            reading2.Join();

            Console.ReadKey();
            //AABBCCDEDEFGHFIJGKHILMJKNOLPMNQORSPQTRSUVTWUXVYWZXYZ
        }

        static void Readind()
        {
            StreamReader sr = new StreamReader(path);

            int b = sr.Read();
            while (b != -1)
            {
                Thread.Sleep(rnd.Next(0, 200));
                Console.Write((char)b);
                b = sr.Read();
            }
        }

        static void CreateTestFile(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    sw.Write((char)i);
                }
            }
        }
    }
}
