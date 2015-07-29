using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class Watermark
    {
        private static UILabel _watermarkComponent;

        static Watermark()
        {
            _watermarkComponent = GameObject.Find("AlphaVersion").GetComponent<UILabel>();
        }

        public static string Text
        {
            get { return _watermarkComponent.text; }
            set { _watermarkComponent.text = value; }
        }
    }
}
