using System.IO;

namespace Stream_StreamWriter_Dispose
{
    class Program
    {
        static void Main()
        {
            StreamWriter sw1 = new StreamWriter("test.txt");

            sw1.Dispose();
            // Без этого создание sw2 вызовет System.IO.IOException:
            //'Процесс не может получить доступ к файлу
            //"E:\VS\CSharp\Stream_StreamWriter_Dispose\bin\Debug\test.txt",
            //так как этот файл используется другим процессом.'

            StreamWriter sw2 = new StreamWriter("test.txt");

            // Нет ошибок.
        }
    }
}
