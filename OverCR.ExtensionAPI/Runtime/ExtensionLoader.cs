using OverCR.ExtensionAPI.Debugging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OverCR.ExtensionAPI.Runtime
{
    internal class ExtensionLoader
    {
        private List<string> _extensionFilePathList;

        internal ExtensionLoader(List<string> extensionFilePathList)
        {
            _extensionFilePathList = extensionFilePathList;
        }

        internal Dictionary<string, IExtension> LoadAll()
        {
            ExtensionManager.SystemLog.WriteLine(Severity.Information, "Extension load procedure in progress...", nameof(ExtensionLoader));

            var extensionDictionary = new Dictionary<string, IExtension>();

            foreach(var path in _extensionFilePathList)
            {
                var assembly = Assembly.LoadFrom(path);
                var types = assembly.GetExportedTypes();

                foreach(var type in types)
                {
                    if (type.GetInterface(typeof(IExtension).Name) == null)
                        continue;

                    var extension = Activator.CreateInstance(type) as IExtension;
                    ExtensionManager.SystemLog.WriteLine(Severity.Information, "Activated an instance of extension:", nameof(ExtensionLoader));
                    ExtensionManager.SystemLog.WriteLine(Severity.Information, $"Extension name: {extension.Name}", nameof(ExtensionLoader));
                    ExtensionManager.SystemLog.WriteLine(Severity.Information, $"Extension author: {extension.Author}", nameof(ExtensionLoader));
                    ExtensionManager.SystemLog.WriteLine(Severity.Information, $"Extension author contact: {extension.Contact}", nameof(ExtensionLoader));

                    if (!extensionDictionary.ContainsKey(path))
                    {
                        ExtensionManager.SystemLog.WriteLine(Severity.Information, "Adding extension to the registry...", nameof(ExtensionLoader));
                        extensionDictionary.Add(extension.Name, extension);
                    }
                    else
                    {
                        ExtensionManager.SystemLog.WriteLine(Severity.Warning, "Extension already exists. How could that happen?", nameof(ExtensionLoader));
                    }
                }
            }
            return extensionDictionary;
        }
    }
}
