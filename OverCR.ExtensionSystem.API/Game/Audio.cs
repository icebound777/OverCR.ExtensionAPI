namespace OverCR.ExtensionSystem.API.Game
{
    public static class Audio
    {
        public delegate void MusicChangedEventHandler(string newTrackName);
        public static event MusicChangedEventHandler CustomMusicChanged;

        static Audio()
        {
            Events.CustomMusicChanged.Subscribe(OnCustomMusicChanged);
        }

        public static string CurrentCustomMusicDirectory
        {
            get { return G.Sys.AudioManager_.CurrentCustomMusicDirectory_; }
            set { G.Sys.AudioManager_.SetCustomMusicDirectory(value); }
        }

        public static string CurrentCustomSongName => G.Sys.AudioManager_.CurrentCustomSong_;

        public static string CurrentCustomSongPath => G.Sys.AudioManager_.CurrentCustomSongPath_;

        public static bool CustomMusicEnabled => G.Sys.AudioManager_.CurrentMusicState_ == AudioManager.MusicState.CustomMusic;

        public static bool RepeatEnabled { get; private set; }

        public static bool ShuffleEnabled { get; private set; }

        public static void EnableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(true);
            G.Sys.OptionsManager_.Audio_.EnableCustomMusic_ = true;
        }

        public static void DisableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(false);
            G.Sys.OptionsManager_.Audio_.EnableCustomMusic_ = false;
        }

        public static void PlayCustomMusic(string customSongName)
        {
            var retryCount = 0;

            if(FileEx.Exists($"{CurrentCustomMusicDirectory}/{customSongName}"))
            {
                while(CurrentCustomSongName != customSongName)
                {
                    if (retryCount >= DirectoryEx.GetFiles(CurrentCustomMusicDirectory).Length)
                        break;

                    NextCustomMusicTrack();

                    retryCount++;
                }
            }
        }

        public static void ToggleRepeat()
        {
            RepeatEnabled = !RepeatEnabled;
            G.Sys.AudioManager_.SetLoopCustomTrack(RepeatEnabled);
            G.Sys.OptionsManager_.Audio_.LoopTrackCustomMusic_ = RepeatEnabled;
        }

        public static void ToggleShuffle()
        {
            ShuffleEnabled = !ShuffleEnabled;
            G.Sys.AudioManager_.SetRandomizeTracks(ShuffleEnabled);
        }

        public static void NextCustomMusicTrack()
        {
            bool toggled = false;

            if (RepeatEnabled)
            {
                ToggleRepeat();
                toggled = true;
            } 

            // Above and below the next line is required, because
            // Distance playlist doesn't work as one would expect.
            // If looping is enabled, incrementation just loops 
            // around one single ID.
            //
            // Because one wants a consistent music player behavior
            // I needed to do this toggle-fu.
            //
            G.Sys.AudioManager_.IncrementCustomMusic(1, RepeatEnabled);

            if(toggled)
                ToggleRepeat();
        }

        public static void PreviousCustomMusicTrack()
        {
            bool toggled = false;

            if (RepeatEnabled)
            {
                ToggleRepeat();
                toggled = true;
            }

            G.Sys.AudioManager_.IncrementCustomMusic(-1, RepeatEnabled);

            if (toggled)
                ToggleRepeat();
        }

        public static void EnableBoomboxMode()
        {
            G.Sys.OptionsManager_.General_.BoomBoxMode_ = true;
        }

        public static void DisableBoomboxMode()
        {
            G.Sys.OptionsManager_.General_.BoomBoxMode_ = false;
        }

        private static void OnCustomMusicChanged(Events.CustomMusicChanged.Data data)
        {
            CustomMusicChanged?.Invoke(data.newTrackName_);
        }
    }
}
