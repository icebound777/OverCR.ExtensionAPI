using OverCR.ExtensionSystem.API.Runtime;
using OverCR.ExtensionSystem.Manager.Debugging;
using OverCR.ExtensionSystem.Manager.Filesystem;
using OverCR.ExtensionSystem.Manager.Runtime;
using System;
using System.IO;
using System.Collections.Generic;
using static OverCR.ExtensionSystem.API.Filesystem.Paths;
using static OverCR.ExtensionSystem.Manager.Filesystem.Initializer;

namespace OverCR.ExtensionSystem.Manager
{
    public class ExtensionManager : IManager
    {
        internal static Log SystemLog { get; private set; }

        private ExtensionScanner _extensionScanner;
        private ExtensionLoader _extensionLoader;

        private List<string> _extensionPathList;

        public Dictionary<string, IExtension> ExtensionRegistry { get; private set; }

        public ExtensionManager()
        {
            ClearAllLogs();

            SystemLog = new Log();
            SystemLog.WriteLine(Severity.Information, "OverCR Distance Extension API initializing...");

            if (InitializationRequired())
            {
                InitializeExtensionFilesystem();
                return;
            }

            ScanForExtensions();
            TryLoadExtensions();

            if (ExtensionRegistry == null)
                return;

            WakeUpExtensions();

            SystemLog.WriteLine(Severity.Information, "Extension initialization complete.");
        }

        private void ClearAllLogs()
        {
            foreach(var path in Directory.GetFiles(ExtensionLogDirectory))
            {
                File.Delete(path);
            }
        }

        private void ScanForExtensions()
        {
            _extensionScanner = new ExtensionScanner(ExtensionDirectory);
            _extensionPathList = _extensionScanner.Scan();
        }

        private void TryLoadExtensions()
        {
            if (_extensionPathList.Count > 0)
            {
                SystemLog.WriteLine(Severity.Success, $"Extension scan finished. Found {_extensionPathList.Count} extensions.");
                _extensionLoader = new ExtensionLoader(_extensionPathList);
                ExtensionRegistry = _extensionLoader.LoadAll();
            }
            else
            {
                SystemLog.WriteLine(Severity.Information, "Extension scan finished. No extensions found.");
                ExtensionRegistry = null;
            }
        }

        private void WakeUpExtensions()
        {
            SystemLog.WriteLine(Severity.Information, "Waking up all loaded extensions...");
            foreach (var extension in ExtensionRegistry?.Values)
            {
                try
                {
                    extension.WakeUp(this);
                }
                catch(Exception ex)
                {
                    SystemLog.WriteLine(Severity.Failure, $"{extension.Name} failed to wake up.");
                    SystemLog.WriteLine(Severity.Failure, $"{ex}");
                }
            }
        }

        public void UpdateExtensions()
        {
            if (ExtensionRegistry != null)
            {
                foreach (var extension in ExtensionRegistry?.Values)
                {
                    try
                    {
                        extension.Update();
                    }
                    catch (Exception ex)
                    {
                        SystemLog.WriteLine(Severity.Failure, "Failed to update an extension.");
                        SystemLog.WriteLine(Severity.Failure, "Exception: ");
                        SystemLog.WriteLine(Severity.Failure, $"{ex}");
                    }
                }
            }
        }
    }
}
