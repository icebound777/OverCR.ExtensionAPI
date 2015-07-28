using static OverCR.ExtensionSystem.API.Filesystem.Paths;
using static System.IO.Directory;

namespace OverCR.ExtensionSystem.Manager.Filesystem
{
    internal static class Initializer
    {
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
