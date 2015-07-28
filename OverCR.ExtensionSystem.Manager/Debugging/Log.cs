using System;
using System.IO;
using static OverCR.ExtensionSystem.API.Filesystem.Paths;
using static OverCR.ExtensionSystem.Manager.Debugging.Severity;

namespace OverCR.ExtensionSystem.Manager.Debugging
{
    internal class Log
    {
        internal string LogFileName { get; set; } = "extensionapi.log";
        internal bool LoggingEnabled { get; set; } = true;

        internal void WriteLine(Severity severity, string message)
        {
            var prefix = "[...]";

            switch(severity)
            {
                case Information:
                    prefix = "[INFO]";
                    break;
                case Success:
                    prefix = "[ OK ]";
                    break;
                case Warning:
                    prefix = "[WARN]";
                    break;
                case Failure:
                    prefix = "[FAIL]";
                    break;
                case Debug:
                    prefix = "[DEBG]";
                    break;
            }

            var messageToWrite = $"{prefix} {DateTime.Now} -> {message}";

            using(var sw = new StreamWriter($"{ExtensionSystemRoot}/{LogFileName}", true))
            {
                lock(LogFileName)
                {
                    sw.WriteLine(messageToWrite);
                }
            }
        }
    }
}
