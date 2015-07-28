﻿using OverCR.ExtensionSystem.API.Runtime;
using OverCR.ExtensionSystem.Manager.Debugging;
using OverCR.ExtensionSystem.Manager.Filesystem;
using OverCR.ExtensionSystem.Manager.Runtime;
using System;
using System.Collections.Generic;
using static OverCR.ExtensionSystem.Manager.Filesystem.Initializer;

namespace OverCR.ExtensionSystem.Manager
{
    public class ExtensionManager
    {
        internal static Log SystemLog { get; private set; }

        private ExtensionScanner _extensionScanner;
        private ExtensionLoader _extensionLoader;

        private List<string> _extensionPathList;
        private Dictionary<string, IExtension> _extensionRegistry;

        // Executed on activation of ExtensionManager instance in Distance's GameManager.ctor
        // See above Instance property for the idea.
        //
        public ExtensionManager()
        {
            if (InitializationRequired())
            {
                InitializeExtensionFilesystem();
                return;
            }

            SystemLog = new Log();
            SystemLog.WriteLine(Severity.Information, "OverCR Distance Extension API initializing...");

            ScanForExtensions();
            TryLoadExtensions();

            if (_extensionRegistry == null)
                return;

            WakeUpExtensions();

            SystemLog.WriteLine(Severity.Information, "Extension initialization complete.");
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
                _extensionRegistry = _extensionLoader.LoadAll();
            }
            else
            {
                SystemLog.WriteLine(Severity.Information, "Extension scan finished. No extensions found.");
                _extensionRegistry = null;
            }
        }

        private void WakeUpExtensions()
        {
            SystemLog.WriteLine(Severity.Information, "Waking up all loaded extensions...");
            foreach (var extension in _extensionRegistry.Values)
            {
                extension.WakeUp();
            }
        }

        // Called from Distance's GameManager.Update();
        //
        private void UpdateExtensions()
        {
            foreach(var extension in _extensionRegistry.Values)
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