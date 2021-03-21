using System;
using UnityEngine;

namespace Unity_Vector3_Vectors
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("name:".PadRight(20) + "(x, y, z)");
            Console.WriteLine("left: ".PadRight(20) + Vector3.left.ToString("n0"));
            Console.WriteLine("up: ".PadRight(20) + Vector3.up.ToString("n0"));
            Console.WriteLine("back: ".PadRight(20) + Vector3.back.ToString("n0"));
            Console.WriteLine("forward: ".PadRight(20) + Vector3.forward.ToString("n0"));
            Console.WriteLine("one: ".PadRight(20) + Vector3.one.ToString("n0"));
            Console.WriteLine("zero: ".PadRight(20) + Vector3.zero.ToString("n0"));
            Console.WriteLine("negativeInfinity: ".PadRight(20) + Vector3.negativeInfinity.ToString("n0"));
            Console.WriteLine("positiveInfinity: ".PadRight(20) + Vector3.positiveInfinity.ToString("n0"));
            Console.WriteLine("down: ".PadRight(20) + Vector3.down.ToString("n0"));
            Console.WriteLine("fwd: ".PadRight(20) + Vector3.fwd.ToString("n0"));

            /*
            name:               (x, y, z)
            left:               (-1, 0, 0)
            up:                 (0, 1, 0)
            back:               (0, 0, -1)
            forward:            (0, 0, 1)
            one:                (1, 1, 1)
            zero:               (0, 0, 0)
            negativeInfinity:   (-Infinity, -Infinity, -Infinity)
            positiveInfinity:   (Infinity, Infinity, Infinity)
            down:               (0, -1, 0)
            fwd:                (0, 0, 1)
            */

            Console.ReadKey();
        }
    }
}
