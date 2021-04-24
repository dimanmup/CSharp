namespace Types_RealsImplicitConversion
{
    class Program
    {
        static void Main()
        {
            // 1
            float f = 1;
            double d = 20;
            decimal m = 300;

            // Навести курсор мыши на var.
            // 2
            var f_result = f + 1; // float
            var d_result = d + f + 1; // double
            var m_result = m + 1; // decimal
            //var x = m + f; Ошибка компиляции.
        }
    }
}
