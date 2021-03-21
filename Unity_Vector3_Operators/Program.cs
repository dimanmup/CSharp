using System;
using UnityEngine;

namespace Unity_Vector3_Operators
{
    class Program
    {
        static void Main()
        {
            Vector3 v1 = new Vector3(2, 4, 6);
            Vector3 v2 = new Vector3(1, 3, 5);

            Console.WriteLine(v1 + v2);
            // (2+1, 4+3, 6+5)
            // (3, 7, 11) 

            Console.WriteLine(v1 - v2);
            // (2-1, 4-3, 6-5)
            // (1, 1, 1) 

            Console.WriteLine(v1 * 2);
            // (2*2, 4*2, 6*2)
            // (4, 8, 12)

            Vector3 v1_ = new Vector3(2, 4, 6);
            Console.WriteLine(v1 == v1_);
            // True

            Console.ReadKey();
        }
    }
}
