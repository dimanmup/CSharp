using Sys = System; // (1)

namespace Using
{
    using Cons = System.Console; // (2)

    class Program
    {
        static void Main()
        {
            Sys.Console.WriteLine("Sys");
            Cons.WriteLine("Cons");
            System.Console.ReadKey();
        }
    }
}