using System.Reflection;

namespace OverCR.ExtensionSystem.API.Runtime
{
    public class Version
    {
        public static class Distance
        {
            public static int Build => SVNRevision.number_;
        }

        public static class ExtensionSystem
        {
            public static int Major => Assembly.GetAssembly(typeof(Version)).GetName().Version.Major;
            public static int Minor => Assembly.GetAssembly(typeof(Version)).GetName().Version.Minor;
        }
    }
}
