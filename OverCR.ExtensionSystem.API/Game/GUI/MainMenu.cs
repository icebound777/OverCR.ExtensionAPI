using System;
using Events.MainMenu;

namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class MainMenu
    {
        public static event EventHandler Loaded;

        static MainMenu()
        {
            Initialized.Subscribe(MainMenu_Initialized);
        }

        private static void MainMenu_Initialized(Initialized.Data data)
        {
            Loaded?.Invoke(null, EventArgs.Empty);
        }
    }
}
