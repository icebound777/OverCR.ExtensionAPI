using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game;
using OverCR.ExtensionSystem.API.Runtime;
using UnityEngine;

namespace OverCR.OnlineNameHider.Extension
{
    public class Entry : IExtension
    {
        public string Name => "Online Name Hider";
        public string Author => "OverCR solutions";
        public string Contact => "overcr@outlook.com";

        private Settings _settings;
        private bool _enabled;
        private string _toggleKey;

        public void WakeUp(IManager manager)
        {
            _settings = new Settings(this);

            if (_settings != null)
            {
                if (!bool.TryParse(_settings["Enabled"], out _enabled))
                    _enabled = false;

                _toggleKey = _settings["ToggleKey"];
            }
            else
            {
                _enabled = false;
                _toggleKey = "F9";
            }
        }

        public void Update()
        {
            if (Keyboard.GetKeyDown(_toggleKey))
            {
                _enabled = !_enabled;
                _settings["ToggleKey"] = _enabled.ToString();
            }

            if (_enabled)
            {
                foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
                {
                    if (gameObject.name == "Name3DText(Clone)")
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                foreach (var gameObject in Object.FindObjectsOfType<GameObject>())
                {
                    if (gameObject.name == "Name3DText(Clone)")
                    {
                        gameObject.SetActive(true);
                    }
                }
            }
        }

        public void Shutdown()
        {
            
        }
    }
}
