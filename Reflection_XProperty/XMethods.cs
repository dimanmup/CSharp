using System;
using System.Linq;
using System.Reflection;

namespace Reflection_XProperty
{
    /// <summary>
    /// Методы расширения для работы со свойствами.
    /// </summary>
    public static class XMethods
    {
        public static bool SetPropertyValue(this object source, string propertyName, object propertyValue)
        {
            PropertyInfo pi = source.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.Name == propertyName);

            if (pi == null)
            {
                return false;
            }

            try
            {
                var piValue = Convert.ChangeType(propertyValue, pi.PropertyType);
                pi.SetValue(source, piValue);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object GetPropertyValue(this object source, string propertyName)
        {
            object value = source.GetType()
                .GetProperties()
                .FirstOrDefault(x => x.Name == propertyName)?.GetValue(source);

            return value;
        }
    }
}
