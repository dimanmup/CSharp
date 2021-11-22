using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace UserAesKV_1
{
    class Program
    {
        static void Main()
        {
            string kStringTrue = "180.192.163.119.77.247.189.159.114.126.126.97.82.55.212.141.69.223.162.47.131.201.27.150.87.54.37.249.178.140.152.152";
            string ivStringTrue = "231.158.141.97.26.152.52.221.133.113.112.96.68.25.154.69";

            string userName = "banana123";

            Aes aes;
            Exception ex;

            if (GenUserAes(userName, out aes, out ex))
            {
                string kString = string.Join(".", aes.Key);
                string ivString = string.Join(".", aes.IV);

                //Console.WriteLine(kString);
                //Console.WriteLine(ivString);

                if (kString == kStringTrue && ivString == ivStringTrue)
                {
                    Console.WriteLine("OK");
                }
            }
            else
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        static bool GenUserAes(string userName, out Aes aes, out Exception ex)
        {
            ex = null;
            aes = Aes.Create();

            int incrementsCount = 1;
            bool pushSide = true;
            
            try
            {
                List<byte> kPre = userName.ToUpper().Select(c => (byte)c).ToList();
                List<byte> kPrePart = new List<byte>();

                while (kPre.Count <= aes.Key.Length)
                {
                    byte kPreLength = (byte)kPre.Count;

                    unchecked
                    {
                        for (int i = 0; i < kPreLength - incrementsCount; i++)
                        {
                            byte next = kPre[i];

                            for (int j = 1; j <= incrementsCount; j++)
                            {
                                next += kPre[i + j];
                                next += kPreLength;
                            }

                            kPrePart.Add(next);
                        }
                    }

                    if (pushSide)
                    {
                        kPre = kPrePart.Concat(kPre).ToList();
                    }
                    else
                    {
                        kPre.AddRange(kPrePart);
                    }

                    kPrePart.Clear();

                    pushSide = !pushSide;
                    incrementsCount++;
                }

                int n = 0;
                IEnumerable<byte> vPre = kPre.TakeLast(aes.IV.Length).Select(vb => (byte)unchecked(vb + (byte)kPre[n++]));

                aes.Key = kPre.Take(aes.Key.Length).ToArray();
                aes.IV = vPre.ToArray();
            }
            catch (Exception e)
            {
                ex = e;
                return false;
            }

            return true;
        }
    }
}
