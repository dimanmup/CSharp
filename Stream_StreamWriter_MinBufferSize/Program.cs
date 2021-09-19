using System.IO;
using System.Text;

namespace Stream_StreamWriter_MinBufferSize
{
    class Program
    {
        internal const int DefaultBufferSize = 1024;
        private const int DefaultFileStreamBufferSize = 4096;
        private const int MinBufferSize = 128;

        static void Main()
        {
            StreamWriter sw = new StreamWriter(
                "test.txt",
                false,
                Encoding.ASCII,
                MinBufferSize - 1);

            for (int i = 1; i <= MinBufferSize + 1; i++)
            {
                sw.Write('.');
            }

            // test.txt имеет MinBufferSize символов, 1 в буфере.
        }
    }
}
