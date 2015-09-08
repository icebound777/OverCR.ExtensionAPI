using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game;
using OverCR.ExtensionSystem.API.Runtime;

namespace OverCR.SpeedTest.Extension
{
    public class Entry : IExtension
    {
        public string Name => "SpeedTest";
        public string Author => "OverCR solutions";
        public string Contact => "overcr@outlook.com";

        private bool _enabled;
        private string _toggleKey;
        private Settings _settings;


        public void WakeUp(IManager manager)
        {
            _settings = new Settings(this);

            if (!bool.TryParse(_settings["Enabled"], out _enabled))
                _enabled = false;

            _toggleKey = _settings["ToggleKey"];
        }

        public void Update()
        {
            if (Keyboard.GetKeyDown(_toggleKey))
            {
                _enabled = !_enabled;
                _settings["Enabled"] = _enabled.ToString();
            }

            if (_enabled)
            {
                ExtensionSystem.API.Game.Vehicle.FloatingHUD.WriteText(
                    1, 
                    0, 
                    $"MPH: {ExtensionSystem.API.Game.Vehicle.Local.MilesPerHour}\n" +
                    $"MPH_B: {ExtensionSystem.API.Game.Vehicle.Local.MilesPerHourB}\n" +
                    $"SMOOTH_SPD: {ExtensionSystem.API.Game.Vehicle.Local.SmoothSpeed}\n" +
                    $"SMOOTH_VEL: {ExtensionSystem.API.Game.Vehicle.Local.SmoothVelocity}",
                    1
                );
            }
        }

        public void Shutdown()
        {

        }
    }
}
