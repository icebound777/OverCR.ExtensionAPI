using OverCR.ExtensionSystem.Manager.Debugging;
using System.Collections.Generic;
using System.Linq;
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
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Extension scan in progress...");
            return GetFiles(_directoryPath, FileTemplate).ToList();
        }
    }
}
