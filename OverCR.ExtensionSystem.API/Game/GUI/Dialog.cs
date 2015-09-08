namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class Dialog
    {
        public enum TextAlignment
        {
            Left,
            Center,
            Right
        }

        public delegate void MessageDialogDelegate();

        public static void ShowMessageDialog(string title, string message, TextAlignment textAlignment = TextAlignment.Center, MessageDialogDelegate actionAfter = null)
        {
            var pivot = UIWidget.Pivot.Center;

            switch (textAlignment)
            {
                case TextAlignment.Left:
                    pivot = UIWidget.Pivot.Left;
                    break;
                case TextAlignment.Center:
                    pivot = UIWidget.Pivot.Center;
                    break;
                case TextAlignment.Right:
                    pivot = UIWidget.Pivot.Right;
                    break;
            }

            G.Sys.MenuPanelManager_?.ShowError(
                message,
                title,
                () => actionAfter?.Invoke(),
                pivot
            );
        }

        public static void ShowDecisionDialog(string title, string message, TextAlignment textAlignment = TextAlignment.Center, MessageDialogDelegate okAction = null, MessageDialogDelegate cancelAction = null)
        {
            var pivot = UIWidget.Pivot.Center;

            switch (textAlignment)
            {
                case TextAlignment.Left:
                    pivot = UIWidget.Pivot.Left;
                    break;
                case TextAlignment.Center:
                    pivot = UIWidget.Pivot.Center;
                    break;
                case TextAlignment.Right:
                    pivot = UIWidget.Pivot.Right; 
                    break;
            }

            G.Sys.MenuPanelManager_?.ShowOkCancel(
                message,
                title,
                () => okAction?.Invoke(),
                () => cancelAction?.Invoke(),
                pivot
            );
        }
    }
}
