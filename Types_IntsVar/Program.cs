namespace Types_IntsVar
{
    class Program
    {
        static void Main()
        {
            // Навести курсор мыши на имя переменной или литерал.

            var i = 1;
            var ui = 2147483648; // Max(int) + 1
            var l = 4294967296; // Max(uint) + 1
            var ul = 9223372036854775808; // Max(long) + 1

            var b = 1; // 1 принадлежит int, но помещается в byte.
        }
    }
}
