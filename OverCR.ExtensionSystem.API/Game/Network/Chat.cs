using System;

namespace OverCR.ExtensionSystem.API.Game.Network
{
    public static class Chat
    {
        public delegate void MessageHandler(string from, string message);
        public static event MessageHandler MessageReceived;
        public static event MessageHandler MessageSent;

        static Chat()
        {
            // Events.Local.Message.Subscribe(OnMessageReceived);
            Events.Local.ChatSubmitMessage.Subscribe(OnMessageSent);
            Events.ClientToAllClients.ChatMessage.Subscribe(OnMessageReceived);
        }

        private static void OnMessageReceived(Events.ClientToAllClients.ChatMessage.Data data)
        {
            var extracted = ExtractData(data.message_);
            if(extracted != null)
                MessageReceived?.Invoke(extracted[0], extracted[1]);
        }

        private static void OnMessageSent(Events.Local.ChatSubmitMessage.Data data)
        {
            MessageSent?.Invoke(G.Sys.ProfileManager_.CurrentProfile_.Name_, data.message_);
        }

        private static string[] ExtractData(string message)
        {
            if(string.IsNullOrEmpty(message))
            {
                return null;
            }

            var splitData = message.Split(':');
            var user = splitData[0];
            user = user.Remove(0, 8);

            if (user.LastIndexOf('[') == -1)
                return null;

            user = user.Remove(user.LastIndexOf('['), 8);

            splitData = splitData.RemoveAt(0);
            var messageString = string.Join("", splitData);

            return new[] { user, messageString };
        }
    }
}
