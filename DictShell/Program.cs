using System;
using System.Collections.Generic;

namespace DictShell
{
    class MyDict
    {
        public Dictionary<string, object> Dict = new Dictionary<string, object>();

        public float Get(string key)
        {
            object val = Dict[key];

            if (val is int)
            {
                return (int)val;
            }    
            
            if (val is string)
            {
                return Get((string)val);
            }

            if (val is Func<float>)
            {
                return ((Func<float>)val).Invoke();
            }

            if (val is Func<string>)
            {
                return Get(((Func<string>)val).Invoke());
            }

            return float.NegativeInfinity;
        }
    }

    class MyObject
    {
        public MyDict MyDict = new MyDict();

        public bool ZSelector = false;

        public MyObject()
        {
            MyDict.Dict = new Dictionary<string, object>
            {
                ["x"] = 11,
                ["y"] = 99,
                ["z"] = (Func<string>)(() => ZSelector ? "x" : "y")
            };
        }
    }

    class Program
    {
        static void Main()
        {
            MyObject mo = new MyObject();

            Console.WriteLine(mo.MyDict.Get("z")); // 99
            
            mo.ZSelector = true;

            Console.WriteLine(mo.MyDict.Get("z")); // 11

            Console.ReadKey();
        }
    }
}
