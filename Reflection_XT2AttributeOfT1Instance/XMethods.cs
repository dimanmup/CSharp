using System;

namespace Reflection_XT2AttributeOfT1Instance
{
    public static class XMethods
    {
        public static T GetAttribute<T>(this object t)
        {
            object[] f = t.GetType().GetCustomAttributes(typeof(T), false);

            if (f != null && f.Length == 1)
            {
                return (T)f[0];
            }

            throw new Exception("Атрибут не найден.");
        }
    }
}
