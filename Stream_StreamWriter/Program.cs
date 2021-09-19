using System;
using System.IO;

namespace Stream_StreamWriter
{
    class Program
    {
        internal const int DefaultBufferSize = 1024;
        private const int DefaultFileStreamBufferSize = 4096;
        private const int MinBufferSize = 128;

        static void Main()
        {
            StreamWriter sw = new StreamWriter("test.txt");

            for (int i = 1; i <= DefaultBufferSize - 1; i++)
            {
                sw.Write('x');
            }
            
            Console.ReadKey(); // Посмотреть файл.
            // test.txt пустой.

            for (int i = 1; i <= 2; i++)
            {
                sw.Write('x');
            }

            // test.txt имеет DefaultBufferSize символов, 1 в буфере.
        }
    }
}
