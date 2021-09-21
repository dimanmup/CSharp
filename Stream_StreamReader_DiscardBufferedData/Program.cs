using System;
using System.IO;

namespace Stream_StreamReader_DiscardBufferedData
{
    class Program
    {
        static void Main()
        {
            int bufferSize = 1024;
            int buffer2Count = 5;
            string path = "test.txt";
            CreateTestFile(path, bufferSize + buffer2Count);

            int bufferByteNumber = 1;
            using (StreamReader sr = new StreamReader(path))
            {
                sr.Read();
                sr.DiscardBufferedData();
                while (sr.Read() != -1)
                {
                    Console.Write("n: " + bufferByteNumber);
                    Console.Write(",\tlast n: " + sr.BaseStream.Position);
                    Console.Write(",\tn is this buffer count: " + (bufferByteNumber == buffer2Count));
                    Console.WriteLine();

                    bufferByteNumber++;
                }
            }

            Console.ReadKey();
            //n: 1,   last n: 1029,   n is this buffer count: False
            //n: 2,   last n: 1029,   n is this buffer count: False
            //n: 3,   last n: 1029,   n is this buffer count: False
            //n: 4,   last n: 1029,   n is this buffer count: False
            //n: 5,   last n: 1029,   n is this buffer count: True
        }

        static void CreateTestFile(string path, int size)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 1; i <= size; i++)
                {
                    sw.Write('x');
                }
            }
        }
    }
}
