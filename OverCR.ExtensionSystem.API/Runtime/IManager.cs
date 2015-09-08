using System.Collections.Generic;

namespace OverCR.ExtensionSystem.API.Runtime
{
    public interface IManager
    {
        Dictionary<string, IExtension> ExtensionRegistry { get; }
    }
}
