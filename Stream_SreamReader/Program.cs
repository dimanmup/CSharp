using System;
using System.IO;

namespace Stream_SreamReader
{
    class Program
    {
        static void Main()
        {
            string path = "AZ.txt";
            CreateTestFile(path);

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() != -1)
                {
                    Console.Write((char)sr.Read());
                }
            }

            Console.ReadKey();
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ
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
