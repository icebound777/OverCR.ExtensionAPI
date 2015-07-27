using OverCR.ExtensionAPI.Debugging;
using System.Collections.Generic;
using System.Linq;
using static System.IO.Directory;

namespace OverCR.ExtensionAPI.Filesystem
{
    internal class ExtensionScanner
    {
        private const string FileTemplate = "*.Extension.dll";
        private readonly string _directoryPath;

        internal ExtensionScanner(string directoryPath)
        {
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Preparing extension scan.", nameof(ExtensionScanner));
            _directoryPath = directoryPath;
        }

        internal List<string> Scan()
        {
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Extension scan in progress...", nameof(ExtensionScanner));
            return GetFiles(_directoryPath, FileTemplate).ToList();
        }
    }
}
