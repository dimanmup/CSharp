using System.IO;
using System.Text;

namespace Stream_StreamWriter_LeaveOpen
{
    class Program
    {
        static void Main()
        {
            string text1 = "Hello";
            string text2 = " World";

            StreamWriter sw = new StreamWriter("test.txt");

            //StreamWriter sw1 = new StreamWriter(sw.BaseStream);
            //StreamWriter sw1 = new StreamWriter(sw.BaseStream, Encoding.UTF8, 1024, false);
            //Создание sw2 вызовет System.ArgumentException: 'Поток был недоступен для записи.'

            StreamWriter sw1 = new StreamWriter(sw.BaseStream, Encoding.UTF8, 1024, true);
            sw1.Write(text1);
            sw1.Dispose();

            StreamWriter sw2 = new StreamWriter(sw.BaseStream);
            sw2.Write(text2);
            sw2.Dispose();
        }
    }
}
