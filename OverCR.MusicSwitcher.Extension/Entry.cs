using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game;
using OverCR.ExtensionSystem.API.Runtime;

namespace OverCR.MusicSwitcher.Extension
{
    public class Entry : IExtension
    {
        public string Author { get; } = "OverCR solutions";
        public string Contact { get; } = "[DATA ERASED]";
        public string Name { get; } = "Music Switcher";

        private string _prevTrackKey;
        private string _nextTrackKey;
        private string _loopToggleKey;
        private string _custMusicToggleKey;
        private string _shuffleToggleKey;
        private bool _writeSongNameOnScreen;

        private Settings _settings;

        public void Update()
        {
            if (Audio.CustomMusicEnabled)
            {
                if (Keyboard.GetKeyDown(_prevTrackKey))
                {
                    Audio.PreviousCustomMusicTrack();
                }

                if (Keyboard.GetKeyDown(_nextTrackKey))
                {
                    Audio.NextCustomMusicTrack();
                }

                if (Keyboard.GetKeyDown(_loopToggleKey))
                {
                    Audio.ToggleRepeat();
                    ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText($"repeat {(Audio.RepeatEnabled ? "on" : "off")}", "00BEAA", 2f);
                }

                if (Keyboard.GetKeyDown(_shuffleToggleKey))
                {
                    Audio.ToggleShuffle();
                    ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText($"shuffle {(Audio.ShuffleEnabled ? "on" : "off")}", "00BEAA", 2f);
                }
            }
            else
            {
                if (Keyboard.GetKeyDown(_prevTrackKey) ||
                    Keyboard.GetKeyDown(_nextTrackKey) ||
                    Keyboard.GetKeyDown(_loopToggleKey))
                {
                    ExtensionSystem.API.Game.Vehicle.Screen.WriteText("Custom music is disabled.\nPlease enable music first.");
                }
            }

            if (Keyboard.GetKeyDown(_custMusicToggleKey))
            {
                if (Audio.CustomMusicEnabled)
                {
                    Audio.DisableCustomMusic();
                    ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText("music disabled", "CC0000", 2f);

                    _settings["MusicWasOn"] = "False";
                }
                else
                {
                    Audio.EnableCustomMusic();
                    ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText("music enabled", "00CC00", 2f);

                    _settings["MusicWasOn"] = "True";
                }
                _settings.Save();
            }
        }

        public void WakeUp(IManager manager)
        {
            _settings = Loader.RetrieveSettings(this);

            _prevTrackKey = _settings["PrevTrackKey"];
            _nextTrackKey = _settings["NextTrackKey"];
            _loopToggleKey = _settings["LoopToggleKey"];
            _shuffleToggleKey = _settings["ShuffleToggleKey"];
            _custMusicToggleKey = _settings["CustomMusicToggleKey"];

            bool.TryParse(_settings["WriteSongNameOnScreen"], out _writeSongNameOnScreen);

            if (!string.IsNullOrEmpty(_settings["MusicWasOn"]))
            {
                bool musicWasOn;
                bool.TryParse(_settings["MusicWasOn"], out musicWasOn);

                if (musicWasOn)
                {
                    Audio.EnableCustomMusic();
                }
                else
                {
                    Audio.DisableCustomMusic();
                }
            }

            if (!string.IsNullOrEmpty(_settings["LastMusicTrackName"]))
            {
                Audio.PlayCustomMusic(_settings["LastMusicTrackName"]);
            }

            Audio.CustomMusicChanged += Audio_CustomMusicChanged;
        }

        private void Audio_CustomMusicChanged(string newTrackName)
        {
            if (newTrackName == "[888888]No track found[-]")
            {
                ExtensionSystem.API.Game.Vehicle.Screen.WriteText("FAILURE:\nSelected track does not exist.");
                ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText("music error", "FF0000", 4.5f);

                return;
            }

            if (newTrackName == "[888888]Folder not found[-]")
            {
                ExtensionSystem.API.Game.Vehicle.Screen.WriteText("FAILURE:\nSelected music folder does not exist.");
                ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText("music error", "FF0000", 4.5f);

                return;
            }

            if (newTrackName == "[888888]Invalid characters in path[-]")
            {
                ExtensionSystem.API.Game.Vehicle.Screen.WriteText("FAILURE:\nPath contains invalid characters.\nVerify your path.");
                ExtensionSystem.API.Game.Vehicle.Screen.WriteTimerText("music error", "FF0000", 4.5f);

                return;
            }
            _settings["LastMusicTrackName"] = newTrackName;
            _settings.Save();

            if (_writeSongNameOnScreen)
            {
                ExtensionSystem.API.Game.Vehicle.Screen.Clear();
                ExtensionSystem.API.Game.Vehicle.Screen.WriteText($"Now playing:\n{newTrackName}");
            }
        }

        public void Shutdown()
        {
            _settings["MusicWasOn"] = Audio.CustomMusicEnabled.ToString();
            _settings["LastMusicTrackName"] = Audio.CurrentCustomSongName;

            _settings.Save();
        }
    }
}
