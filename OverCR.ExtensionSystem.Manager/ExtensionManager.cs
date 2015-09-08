using OverCR.ExtensionSystem.API.Runtime;
using OverCR.ExtensionSystem.Manager.Debugging;
using OverCR.ExtensionSystem.Manager.Filesystem;
using OverCR.ExtensionSystem.Manager.Runtime;
using System;
using System.IO;
using System.Collections.Generic;
using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game.GUI;
using static OverCR.ExtensionSystem.API.Filesystem.Paths;
using static OverCR.ExtensionSystem.Manager.Filesystem.Initializer;

namespace OverCR.ExtensionSystem.Manager
{
    public class ExtensionManager : IManager
    {
        internal static Log SystemLog { get; private set; }

        private ExtensionScanner _extensionScanner;
        private ExtensionLoader _extensionLoader;
        private Settings _settings;

        private List<string> _extensionPathList;

        private bool _showWatermark = true;
        private bool _appendExtensionSystemInfo = true;
        private bool _showDisclaimer = true;

        public Dictionary<string, IExtension> ExtensionRegistry { get; private set; }

        public ExtensionManager()
        {
            ClearAllLogs();
            LoadSettings();
            ApplySettings();

            SystemLog = new Log();
            SystemLog.WriteLine(Severity.Information, "OverCR Distance Extension API initializing...");

            if (InitializationRequired())
            {
                InitializeExtensionFilesystem();
                return;
            }

            if (_showDisclaimer)
            {
                MainMenu.Loaded += (sender, args) =>
                {
                    Dialog.ShowMessageDialog(
                        "DISCLAIMER",
                        "[00A2DF]Distance Extension System[-] is now [00DD00]active[-]. Please note that:\n" +
                        "- Refract is [FF0000]not liable[-] for any damages caused by mods you use.\n" +
                        "- Bug reports from modified Distance builds are [FF0000]not supported[-].\n" +
                        "- By using the Extension System you agree to the above.\n\n" +
                        "If Distance stops working after extension installation, check the Logs folder for details.",
                        Dialog.TextAlignment.Left,
                        () =>
                        {
                            _settings["ShowDisclaimer"] = "False";
                        }
                    );
                };
            }

            ScanForExtensions();
            TryLoadExtensions();

            if (ExtensionRegistry == null)
                return;

            WakeUpExtensions();
            Events.Game.QuitGame.Subscribe(ShutdownExtensions);

            SystemLog.WriteLine(Severity.Information, "Extension initialization complete.");
        }

        private static void ClearAllLogs()
        {
            foreach (var path in Directory.GetFiles(ExtensionLogDirectory, "*.log"))
            {
                File.Delete(path);
            }
        }

        private void LoadSettings()
        {
            _settings = Loader.RetrieveSettings(this);

            if (_settings != null)
            {
                if (string.IsNullOrEmpty(_settings["ShowWatermark"]))
                {
                    _settings["ShowWatermark"] = "True";
                }

                if (string.IsNullOrEmpty(_settings["AppendExtensionSystemInfo"]))
                {
                    _settings["AppendExtensionSystemInfo"] = "True";
                }

                bool.TryParse(_settings["ShowWatermark"], out _showWatermark);
                bool.TryParse(_settings["AppendExtensionSystemInfo"], out _appendExtensionSystemInfo);
                bool.TryParse(_settings["ShowDisclaimer"], out _showDisclaimer);
            }
        }

        private void ApplySettings()
        {
            Watermark.Enabled = _showWatermark;

            if (_appendExtensionSystemInfo)
                Watermark.Text += "\ndistance extension system active";
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
            if (ExtensionRegistry == null)
            {
                SystemLog.WriteLine(Severity.Information, "No extensions to wake up. Skipping.");
                return;
            }

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

        private void ShutdownExtensions(Events.Game.QuitGame.Data data)
        {
            if (ExtensionRegistry == null)
            {
                SystemLog.WriteLine(Severity.Information, "No extensions to shut down. Goodbye!");
                return;
            }
            SystemLog.WriteLine(Severity.Information, "Shutting down all loaded extensions...");
            foreach(var extension in ExtensionRegistry?.Values)
            {
                try
                {
                    extension.Shutdown();
                }
                catch(Exception ex)
                {
                    SystemLog.WriteLine(Severity.Failure, $"{extension.Name} has thrown an exception while shutting down.");
                    SystemLog.WriteLine(Severity.Failure, $"{ex}");
                }
            }
        }

        public void UpdateExtensions()
        {
            if (ExtensionRegistry == null)
                return;

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
