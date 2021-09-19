using System;
using System.IO;

namespace Stream_SreamReaderOnSreamReader
{
    class Program
    {
        static void Main()
        {
            string path = "AZ.txt";
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 'A'; i <= 'Z'; i++)
                {
                    sw.Write((char)i);
                }
            }

            using (StreamReader sr1 = new StreamReader(path))
            using (StreamReader sr2 = new StreamReader(sr1.BaseStream))
            {
                while (sr2.Peek() != -1)
                {
                    Console.Write((char)sr2.Read());
                }
            }

            Console.ReadKey();
            //ABCDEFGHIJKLMNOPQRSTUVWXYZ
        }
    }
}
