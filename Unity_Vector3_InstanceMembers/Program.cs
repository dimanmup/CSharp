using System;
using UnityEngine;

namespace Unity_Vector3_InstanceMembers
{
    class Program
    {
        static void Main()
        {
            Vector3 v = new Vector3(1, 2, 2);
            
            v.z++;

            Console.WriteLine("v.x: " + v.x);
            Console.WriteLine("v.y: " + v.y);
            Console.WriteLine("v.z: " + v.z);

            // Длина.
            double vLength = Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            Console.WriteLine("|v|: " + vLength);
            Console.WriteLine("|v|: " + v.magnitude);

            // Длина^2.
            double vLength2 = v.x * v.x + v.y * v.y + v.z * v.z;
            Console.WriteLine("|v|^2: " + vLength2);
            Console.WriteLine("|v|^2: " + v.sqrMagnitude);

            // Единичный вектор.
            Vector3 v_e = new Vector3(
                (float)(v.x / vLength),
                (float)(v.y / vLength),
                (float)(v.z / vLength));
            Console.WriteLine("v_e: " + v_e);
            Console.WriteLine("v_e: " + v.normalized);
            Console.WriteLine("|v_e|: " + v.normalized.magnitude);

            Console.ReadKey();
        }
    }
}