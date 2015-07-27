using System;
using System.IO;
using static OverCR.ExtensionAPI.Debugging.Severity;
using static OverCR.ExtensionAPI.Filesystem.Initializer;

namespace OverCR.ExtensionAPI.Debugging
{
    internal class Log
    {
        internal string LogFileName { get; set; } = "extensionapi.log";
        internal bool LoggingEnabled { get; set; } = true;

        internal void WriteLine(Severity severity, string message, string owner = "ExtensionAPI")
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

            var messageToWrite = $"{prefix} {DateTime.Now} {owner} -> {message}";

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
