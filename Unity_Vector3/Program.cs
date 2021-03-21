using UnityEngine;
using System;

namespace Unity_Vector3
{
    class Program
    {
        static void Main()
        {
            Vector3 v3_000 = new Vector3();
            Vector3 v3_xy0 = new Vector3(1.5f, 2.7f);
            Vector3 v3_xyz = new Vector3(1f, 2f, 3f);

            Console.WriteLine(v3_000); // (0,0, 0,0, 0,0)
            Console.WriteLine(v3_xy0); // (1,5, 2,7, 0,0)
            Console.WriteLine(v3_xyz); // (1,0, 2,0, 3,0)

            Console.ReadKey();
        }
    }
}
