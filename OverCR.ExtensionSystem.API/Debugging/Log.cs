using System;
using System.IO;
using static OverCR.ExtensionSystem.API.Filesystem.Paths;

namespace OverCR.ExtensionSystem.API.Debugging
{
    public static class Log
    {
        public static void WriteLine(object caller, string message)
        {
            var callerName = caller.GetType().FullName;
            string msg = $"{callerName} {DateTime.Now} -> {message}";

            using (var sw = new StreamWriter(Path.Combine(ExtensionLogDirectory, $"{callerName}.log")))
            {
                sw.WriteLine(msg);
            }
        }
    }
}
