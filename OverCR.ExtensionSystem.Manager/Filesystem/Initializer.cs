using UnityEngine;
using static System.IO.Directory;

namespace OverCR.ExtensionSystem.Manager.Filesystem
{
    internal static class Initializer
    {
        internal static string ExtensionSystemRoot { get; } = $"{Application.dataPath}/ExtensionSystem";
        internal static string ExtensionDirectory { get; } = $"{ExtensionSystemRoot}/Plug-ins";
        internal static string ExtensionSettingsDirectory { get; } = $"{ExtensionSystemRoot}/Settings";

        internal static void InitializeExtensionFilesystem()
        {
            if(!Exists(ExtensionDirectory))
            {
                CreateDirectory(ExtensionDirectory);
            }

            if(!Exists(ExtensionSettingsDirectory))
            {
                CreateDirectory(ExtensionSettingsDirectory);
            }
        }

        internal static bool InitializationRequired()
        {
            return !(Exists(ExtensionDirectory) && Exists(ExtensionSettingsDirectory));
        }
    }
}
