namespace OverCR.ExtensionSystem.API.Game
{
    public static class Audio
    {
        private static bool _repeatCustomMusic;
        private static bool _shuffleCustomMusic;

        public static bool CustomMusicEnabled
        {
            get { return G.Sys.AudioManager_.CurrentMusicState_ == AudioManager.MusicState.CustomMusic; }
        }

        public static string CurrentCustomMusicDirectory
        {
            get { return G.Sys.AudioManager_.CurrentCustomMusicDirectory_; }
            set { G.Sys.AudioManager_.SetCustomMusicDirectory(value); }
        }

        public static string CurrentCustomSongName
        {
            get { return G.Sys.AudioManager_.CurrentCustomSong_; }
        }

        public static string CurrentCustomSongPath
        {
            get { return G.Sys.AudioManager_.CurrentCustomSongPath_; }
        }

        public static bool RepeatEnabled
        {
            get { return _repeatCustomMusic; }
        }

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

        public static void PlayCustomMusic()
        {
            G.Sys.AudioManager_.PlayCustomMusic(G.Sys.AudioManager_.CurrentCustomSongPath_);
        }

        public static void ToggleRepeat()
        {
            _repeatCustomMusic = !_repeatCustomMusic;
            G.Sys.AudioManager_.SetLoopCustomTrack(_repeatCustomMusic);
            G.Sys.OptionsManager_.Audio_.LoopTrackCustomMusic_ = _repeatCustomMusic;
        }

        public static void ToggleShuffle()
        {
            _shuffleCustomMusic = !_shuffleCustomMusic;
            G.Sys.AudioManager_.SetRandomizeTracks(_shuffleCustomMusic);
        }

        public static void NextCustomMusicTrack()
        {
            bool toggled = false;

            if (_repeatCustomMusic)
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
            G.Sys.AudioManager_.IncrementCustomMusic(1, _repeatCustomMusic);

            if(toggled)
                ToggleRepeat();
        }

        public static void PreviousCustomMusicTrack()
        {
            bool toggled = false;

            if (_repeatCustomMusic)
            {
                ToggleRepeat();
                toggled = true;
            }

            G.Sys.AudioManager_.IncrementCustomMusic(-1, _repeatCustomMusic);

            if (toggled)
                ToggleRepeat();
        }

        // BUG: Not working in 3770 (they broke it)
        public static void EnableBoomboxMode()
        {
            G.Sys.OptionsManager_.General_.BoomBoxMode_ = true;
        }

        // BUG: Not working in 3770 (they broke it)
        public static void DisableBoomboxMode()
        {
            G.Sys.OptionsManager_.General_.BoomBoxMode_ = false;
        }
    }
}
