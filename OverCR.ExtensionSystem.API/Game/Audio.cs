using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverCR.ExtensionSystem.API.Game
{
    public static class Audio
    {
        public static void EnableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(true);
        }

        public static void DisableCustomMusic()
        {
            G.Sys.AudioManager_.EnableCustomMusic(false);
        }
    }
}
