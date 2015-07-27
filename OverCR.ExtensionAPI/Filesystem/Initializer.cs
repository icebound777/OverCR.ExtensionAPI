using OverCR.ExtensionAPI.Debugging;
using UnityEngine;
using static System.IO.Directory;

namespace OverCR.ExtensionAPI.Filesystem
{
    internal static class Initializer
    {
        internal static string ExtensionSystemRoot { get; } = $"{Application.dataPath}/ExtensionSystem";
        internal static string ExtensionDirectory { get; } = $"{ExtensionSystemRoot}/Plug-ins";
        internal static string ExtensionSettingsDirectory { get; } = $"{ExtensionSystemRoot}/Settings";

        internal static void InitializeExtensionFilesystem()
        {
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Filesystem initialization requested. Initializing.");
            if (!Exists(ExtensionSystemRoot))
            {
                ExtensionManager.SystemLog.WriteLine(Severity.Information, "Extension system root directory not found. Creating.");
                CreateDirectory(ExtensionSystemRoot);
            }

            if(!Exists(ExtensionDirectory))
            {
                ExtensionManager.SystemLog.WriteLine(Severity.Information, "Plug-in container directory not found. Creating.");
                CreateDirectory(ExtensionDirectory);
            }

            if(!Exists(ExtensionSettingsDirectory))
            {
                ExtensionManager.SystemLog.WriteLine(Severity.Information, "Settings directory not found. Creating.");
                CreateDirectory(ExtensionSettingsDirectory);
            }
        }

        internal static bool InitializationRequired()
        {
            return !(Exists(ExtensionSystemRoot) && Exists(ExtensionDirectory) && Exists(ExtensionSettingsDirectory));
        }
    }
}
