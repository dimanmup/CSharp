using Reflection_XT2AttributeOfT1Instance;
using System;
using System.ComponentModel;

namespace Reflection_XT2AttributeOfT1InstanceExe
{
    [Description("Class A.")]
    public class A 
    { 
    }

    class Program
    {
        static void Main()
        {
            A a = new A();

            DescriptionAttribute aDesc = a.GetAttribute<DescriptionAttribute>();

            Console.WriteLine(aDesc.Description); // Class A.
            Console.ReadKey();
        }
    }
}
