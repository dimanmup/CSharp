namespace Types_Boolean
{
    class Program
    {
        static void Main()
        {
            System.Boolean f = 0 > 1;
            bool t = 0 < 1;

            System.Console.WriteLine(false);
            System.Console.WriteLine(bool.FalseString);
            System.Console.WriteLine(f);

            System.Console.WriteLine(true);
            System.Console.WriteLine(bool.TrueString);
            System.Console.WriteLine(t);

            System.Console.ReadKey();

            //False
            //False
            //False
            //True
            //True
            //True
        }
    }
}
