using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverCR.ExtensionSystem.API.Runtime
{
    public interface IManager
    {
        Dictionary<string, IExtension> ExtensionRegistry { get; }
    }
}
