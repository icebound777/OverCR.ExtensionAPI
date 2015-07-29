namespace OverCR.ExtensionSystem.API.Game
{
    public static class Audio
    {
        private static bool _repeatCustomMusic;

        public static bool CustomMusicEnabled
        {
            get { return G.Sys.AudioManager_.CurrentMusicState_ == AudioManager.MusicState.CustomMusic; }
        }

        public static void EnableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(true);
        }

        public static void DisableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(false);
        }

        public static void PlayCustomMusic()
        {
            G.Sys.AudioManager_.PlayCustomMusic(G.Sys.AudioManager_.CurrentCustomSongPath_);
        }

        public static void ToggleRepeat()
        {
            _repeatCustomMusic = !_repeatCustomMusic;
            G.Sys.AudioManager_.SetLoopCustomTrack(_repeatCustomMusic);
        }

        public static void NextCustomMusicTrack()
        {
            G.Sys.AudioManager_.IncrementCustomMusic(1, _repeatCustomMusic);
        }

        public static void PreviousCustomMusicTrack()
        {
            G.Sys.AudioManager_.IncrementCustomMusic(-1, _repeatCustomMusic);
        }
    }
}
