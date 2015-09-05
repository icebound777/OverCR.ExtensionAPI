namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class Dialog
    {
        public delegate void MessageDialogDelegate();

        public static void ShowMessageDialog(string title, string message, MessageDialogDelegate actionAfter = null)
        {
            G.Sys.MenuPanelManager_?.ShowError(
                message,
                title,
                () => actionAfter?.Invoke()
            );
        }

        public static void ShowDecisionDialog(string title, string message, MessageDialogDelegate okAction = null, MessageDialogDelegate cancelAction = null)
        {
            G.Sys.MenuPanelManager_?.ShowOkCancel(
                message,
                title,
                () => okAction?.Invoke(),
                () => cancelAction?.Invoke()
            );
        }
    }
}
