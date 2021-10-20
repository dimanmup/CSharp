using System;
using System.Reflection;

namespace Neomaster.TestLib
{
    /// <summary>
    /// Demo.
    /// </summary>
    public static class Demo
    {
        /// <summary>
        /// Prints assemby name and version.
        /// </summary>
        public static void Info()
        {
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name + ": " + Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
