using Reflection_MyTypeBuilder;
using Reflection_XProperty;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Reflection_BuildTypeAndItsInstance
{
    public class Person1
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // Static.

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();

            Person1 p1 = new Person1
            {
                Name = "Vasya",
                Age = 5
            };

            string p1Name = p1.Name;
            int p1Age = p1.Age;

            sw1.Stop();
            Console.WriteLine("sw1: " + sw1.ElapsedTicks);



            // Dynamic.

            string typeName = "Person2";
            Dictionary<string, Type> propertiesDefs = new Dictionary<string, Type>
            {
                { "Name", typeof(string) },
                { "Age", typeof(int) }
            };

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();

            object p2 = MyTypeBuilder.CreateInstance(typeName, propertiesDefs);

            p2.SetPropertyValue("Name", "Vasya");
            p2.SetPropertyValue("Age", 5);

            string p2Name = (string)p2.GetPropertyValue("Name");
            int p2Age = (int)p2.GetPropertyValue("Age");

            sw2.Stop();

            Console.WriteLine("sw2: " + sw2.ElapsedTicks);
            Console.WriteLine("sw2/sw1: " + sw2.ElapsedTicks / sw1.ElapsedTicks);
            
            Console.WriteLine($"\np2.Name: {p2Name}, p2.Age: {p2Age}");
            
            /*
            sw1: 3815
            sw2: 105567
            sw2/sw1: 27

            p2.Name: Vasya, p2.Age: 5
            */

            Console.ReadKey();
        }
    }
}
