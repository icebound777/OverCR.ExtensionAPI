﻿using OverCR.ExtensionAPI.Debugging;
using OverCR.ExtensionAPI.Filesystem;
using OverCR.ExtensionAPI.Runtime;
using System.Collections.Generic;
using static OverCR.ExtensionAPI.Filesystem.Initializer;

namespace OverCR.ExtensionAPI
{
    public class ExtensionManager
    {
        internal static Log SystemLog { get; private set; }

        private static ExtensionManager _extensionManager;
        private static readonly object Lock = null;

        private ExtensionScanner _extensionScanner;
        private ExtensionLoader _extensionLoader;

        private List<string> _extensionPathList;
        private Dictionary<string, IExtension> _extensionRegistry;

        // Singleton pattern - required here, because Unity tends to call constructors twice.
        // And then there can be a strange behavior of two extension managers running in parallel. This is not good.
        //
        public static ExtensionManager Instance
        {
            get
            {
                lock (Lock)
                {
                    return _extensionManager ?? (_extensionManager = new ExtensionManager());
                }
            }
        }

        // Executed on activation of ExtensionManager instance in Distance's GameManager.ctor
        // See above Instance property for the idea.
        //
        private ExtensionManager()
        {
            SystemLog = new Log();
            SystemLog.WriteLine(Severity.Information, "OverCR Distance Extension API initializing...");

            if (InitializationRequired())
            {
                InitializeExtensionFilesystem();
                return;
            }

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
                extension.Update();
            }
        }
    }
}
