using UnityEngine;

namespace OverCR.ExtensionSystem.API.Filesystem
{
    public static class Paths
    {
        public static string ExtensionSystemRoot { get; } = $"{Application.dataPath}/ExtensionSystem";
        public static string ExtensionDirectory { get; } = $"{ExtensionSystemRoot}/Plug-ins";
        public static string ExtensionSettingsDirectory { get; } = $"{ExtensionSystemRoot}/Settings";
    }
}
