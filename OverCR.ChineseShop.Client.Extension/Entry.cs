using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game;
using OverCR.ExtensionSystem.API.Game.GUI;
using OverCR.ExtensionSystem.API.Game.Network;
using OverCR.ExtensionSystem.API.Runtime;
using OverCR.ExtensionSystem.API.Game.Vehicle;

namespace OverCR.ChineseShop.Client.Extension
{
    public class Entry : IExtension
    {
        public string Name => "China Shop Client";
        public string Author => "OverCR solutions";
        public string Contact => "overcr@outlook.com";

        private bool _enabled;
        private bool _isBlue;
        private bool _canBreak;

        private bool _showActivityDialog;

        private string _toggleKey;
        private string _teamSwitchKey;

        private Settings _settings;

        public void WakeUp(IManager manager)
        {
            InitializeSettings();

            MainMenu.Loaded += MainMenu_Loaded;
            if (_enabled)
            {
                _canBreak = true;

                Local.CarBrokeObject += Local_CarBrokeObject;
                Local.CarDestroyed += Object_CarDestroyed;
                Local.CarRespawned += Object_CarRespawned;
            }
        }

        private void MainMenu_Loaded(object sender, System.EventArgs e)
        {
            if (_enabled && _showActivityDialog)
            {
                Dialog.ShowMessageDialog(
                    "CHINA SHOP MINI-GAME",
                    $"Please read this message, because it won't appear again.\n\nPlug-in enable/disable: {_toggleKey}\nSwitch teams: {_teamSwitchKey}\n\nYou can rebind these keys by editing configuration file."
                );
                _settings["ShowActivityDialog"] = "False";
            }
        }

        public void Update()
        {
            if (!_enabled)
            {
                if (Keyboard.GetKeyDown(_toggleKey))
                    EnableFunctionality();

                return;
            }

            if (Keyboard.GetKeyDown(_toggleKey))
            {
                DisableFunctionality();

                return;
            }

            if (Keyboard.GetKeyDown(_teamSwitchKey))
            {
                _isBlue = !_isBlue;

                Chat.SendActionMessage(_isBlue
                    ? " has joined team [0054FF]BLUE[-]."
                    : " has joined team [FF3B3B]RED[-].");

                _settings["IsBlue"] = _isBlue.ToString();
            }
        }

        public void Shutdown()
        {
            Local.CarBrokeObject -= Local_CarBrokeObject;
        }

        private void InitializeSettings()
        {
            _settings = Loader.RetrieveSettings(this);

            if (!bool.TryParse(_settings["ClientSideEnabled"], out _enabled))
                _enabled = false;

            if (!bool.TryParse(_settings["ShowActivityDialog"], out _showActivityDialog))
                _showActivityDialog = false;

            if (!bool.TryParse(_settings["IsBlue"], out _isBlue))
                _isBlue = false;
            
            _toggleKey = _settings["ToggleKey"];
            _teamSwitchKey = _settings["TeamSwitchKey"];
        }

        private void DisableFunctionality()
        {
            _enabled = false;
            _settings["ClientSideEnabled"] = "False";

            Local.CarBrokeObject -= Local_CarBrokeObject;
        }


        private void EnableFunctionality()
        {
            _enabled = true;
            _settings["ClientSideEnabled"] = "True";

            Local.CarBrokeObject += Local_CarBrokeObject;
        }

        private void Local_CarBrokeObject(int objectIndex)
        {
            if(!_enabled)
                return;

            if (_canBreak)
            {
                Chat.SendActionMessage(_isBlue ? " [0054FF]BLUE[-]" : " [FF3B3B]RED[-]");
            }
        }

        private void Object_CarDestroyed(string cause)
        {
            _canBreak = false;
        }

        private void Object_CarRespawned(float px, float py, float pz, float rx, float ry, float rz, bool fastRespawn)
        {
            _canBreak = true;
        }
    }
}
