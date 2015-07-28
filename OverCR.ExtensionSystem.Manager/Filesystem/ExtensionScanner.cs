using OverCR.ExtensionSystem.Manager.Debugging;
using System.Collections.Generic;
using static System.IO.Directory;

namespace OverCR.ExtensionSystem.Manager.Filesystem
{
    internal class ExtensionScanner
    {
        private const string FileTemplate = "*.Extension.dll";
        private readonly string _directoryPath;

        internal ExtensionScanner(string directoryPath)
        {
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Preparing extension scan.");
            _directoryPath = directoryPath;
        }

        internal List<string> Scan()
        {
            var list = new List<string>();

            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Extension scan in progress...");
            foreach(var file in GetFiles(_directoryPath, FileTemplate))
            {
                list.Add(file);
            }

            return list;
        }
    }
}
