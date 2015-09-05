using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverCR.ExtensionSystem.API.Configuration;
using OverCR.ExtensionSystem.API.Game.Network;
using OverCR.ExtensionSystem.API.Runtime;

namespace OverCR.ChineseShop.Server.Extension
{
    public class Entry : IExtension
    {
        public string Name => "China Shop Server";
        public string Author => "OverCR solutions";
        public string Contact => "overcr@outlook.com";

        private int _redLimit;
        private int _redScore;
        private int _blueLimit;
        private int _blueScore;
        private Settings _settings;
        private Dictionary<string, int> _playerScores; 

        public void WakeUp(IManager manager)
        {
            Chat.MessageReceived += Chat_MessageReceived;
        }

        private void Chat_MessageReceived(string from, string message)
        {
            var split = message.Split(' ');

            if (_playerScores.ContainsKey(split[0]))
            {
                
            }
        }

        public void Update()
        {
            
        }

        public void Shutdown()
        {
            
        }

        private void InitializeSettings()
        {
            _settings = Loader.RetrieveSettings(this);

            if (!int.TryParse(_settings["BluePointsLimit"], out _blueLimit))
                _blueLimit = 50;
            if (!int.TryParse(_settings["RedPointsLimit"], out _redLimit))
                _redLimit = 50;
        }
    }
}
