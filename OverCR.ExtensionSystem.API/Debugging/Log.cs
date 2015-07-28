using System;
using System.IO;

namespace OverCR.ExtensionSystem.API.Debugging
{
    public static class Log
    {
        public static void WriteLine(object caller, string message)
        {
            var callerName = caller.GetType().FullName;
            string msg = $"{callerName} {DateTime.Now} -> {message}";

            using (var sw = new StreamWriter("extension_log.txt"))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
