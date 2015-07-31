using System.Reflection;

namespace OverCR.ExtensionSystem.API.Runtime
{
    public class Version
    {
        public static class Distance
        {
            public static int Build
            {
                get { return SVNRevision.number_; }
            }
        }

        public static class ExtensionSystem
        {
            public static int Major
            {
                get { return Assembly.GetAssembly(typeof(Version)).GetName().Version.Major; }
            }

            public static int Minor
            {
                get { return Assembly.GetAssembly(typeof(Version)).GetName().Version.Minor; }
            }
        }
    }
}
