using Events;
using Events.Local;

namespace OverCR.ExtensionSystem.API.Game.Network
{
    public static class Chat
    {
        public delegate void MessageHandler(string from, string message);
        public static event MessageHandler MessageReceived;
        public static event MessageHandler MessageSent;

        private static bool _justSent;

        static Chat()
        {
            ChatSubmitMessage.Subscribe(OnMessageSent);
            Events.ClientToAllClients.ChatMessage.Subscribe(OnMessageReceived);
        }

        public static void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            StaticEvent<ChatSubmitMessage.Data>.Broadcast(new ChatSubmitMessage.Data(message));
        }

        public static void SendActionMessage(string action)
        {
            if (string.IsNullOrEmpty(action))
                return;

            StaticEvent<PlayerActionMessage.Data>.Broadcast(new PlayerActionMessage.Data(action));
        }

        private static void OnMessageReceived(Events.ClientToAllClients.ChatMessage.Data data)
        {
            if (_justSent)
            {
                _justSent = false;
                return;
            }
            var extracted = ExtractData(data.message_);

            MessageReceived?.Invoke(extracted?[0], extracted?[1]);
        }

        private static void OnMessageSent(ChatSubmitMessage.Data data)
        {
            if (string.IsNullOrEmpty(data.message_))
                return;

            _justSent = true;
            MessageSent?.Invoke(G.Sys.ProfileManager_.CurrentProfile_.Name_, data.message_);
        }

        private static string[] ExtractData(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return null;
            }

            if (message.IndexOf(':') == -1)
                return new[] { "", message };

            var splitData = message.Split(':');
            var user = splitData[0];
            user = user.Remove(0, 8);

            if (user.LastIndexOf('[') == -1)
            {
                return null;
            }

            user = user.Remove(user.LastIndexOf('['), 8);

            splitData = splitData.RemoveAt(0);
            var messageString = string.Join("", splitData);

            return string.IsNullOrEmpty(messageString) ? null : new[] { user, messageString };
        }
    }
}
