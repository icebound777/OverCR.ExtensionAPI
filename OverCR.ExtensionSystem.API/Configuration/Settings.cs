using System.Collections.Generic;
using System.IO;
using static OverCR.ExtensionSystem.API.Filesystem.Paths;

namespace OverCR.ExtensionSystem.API.Configuration
{
    public class Settings : Dictionary<string, string>
    {
        private readonly object _owner;

        public new string this[string key]
        {
            get
            {
                return ContainsKey(key) ? base[key] : string.Empty;
            }
            set
            {
                if (ContainsKey(key))
                    base[key] = value;
                else
                    Add(key, value);

                Save();
            }
        }

        public Settings() { }

        public Settings(object owner)
        {
            _owner = owner;
            Initialize();      
        }

        public void Save()
        { 
            using (var sw = new StreamWriter($"{ExtensionSettingsDirectory}/{GetFileName()}", false))
            {
                foreach (var pair in this)
                {
                    sw.WriteLine($"{pair.Key}={pair.Value}");
                }
            }
        }

        public static Settings LoadFrom(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            {
                return Parse(sr.ReadToEnd());
            }
        }

        public static Settings Parse(string source)
        {
            var settings = new Settings();

            foreach(var str in source.Split('\n'))
            {
                var trimmed = str.Trim();

                if (trimmed.IndexOf('=') == -1)
                    continue;

                var statement = trimmed.Split('=');

                var key = statement[0];
                statement = statement.RemoveAt(0);

                var value = string.Join("", statement);

                settings.Add(key, value);
            }

            return settings;
        }

        public static bool Exist(object caller)
        {
            return File.Exists($"{ExtensionSettingsDirectory}/{GetFileName(caller)}");
        }

        private string GetFileName()
        {
            return $"{_owner.GetType().Namespace}.cfg";
        }

        private static string GetFileName(object caller)
        {
            return $"{caller.GetType().Namespace}.cfg";
        }

        private void Initialize()
        {
            var settings = LoadFrom($"{ExtensionSettingsDirectory}/{GetFileName()}");
            foreach (var pair in settings)
            {
                Add(pair.Key, pair.Value);
            }
        }
    }
}
