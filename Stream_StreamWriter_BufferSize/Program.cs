using System.IO;
using System.Text;

namespace Stream_StreamWriter_BufferSize
{
    class Program
    {
        internal const int DefaultBufferSize = 1024;
        private const int DefaultFileStreamBufferSize = 4096;
        private const int MinBufferSize = 128;

        static void Main()
        {
            int bufferSize = 150;

            StreamWriter sw = new StreamWriter(
                "test.txt", 
                false, 
                Encoding.ASCII,
                bufferSize);

            for (int i = 1; i <= bufferSize + 1; i++)
            {
                sw.Write('*');
            }

            // test.txt имеет bufferSize символов, 1 в буфере.
        }
    }
}
