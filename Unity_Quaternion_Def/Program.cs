using System;
using System.Numerics;

namespace Unity_Quaternion_Def
{
    class Program
    {
        static void Main()
        {
            Vector4 v = new Vector4(2.22f, 3.33f, 4.44f, 55.55f);

            Quaternion qFromV = new Quaternion(
                v.X * (float)Math.Sin(v.W / 2 * Math.PI / 180),
                v.Y * (float)Math.Sin(v.W / 2 * Math.PI / 180),
                v.Z * (float)Math.Sin(v.W / 2 * Math.PI / 180),
                (float)Math.Cos(v.W / 2 * Math.PI / 180)
                );

            float aFromQ = (float)(Math.Acos(qFromV.W) * 2 * 180 / Math.PI);
            float sin = (float)Math.Sin(aFromQ / 2 * Math.PI / 180);
            Vector4 vFromQ = new Vector4(
                qFromV.X / sin,
                qFromV.Y / sin,
                qFromV.Z / sin,
                aFromQ);

            Console.WriteLine(v);
            Console.WriteLine(vFromQ);
            Console.ReadKey();
            //<2,22  3,33  4,44  55,55>
            //<2,22  3,33  4,44  55,55>
        }
    }
}
