using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class Watermark
    {
        private static UILabel _watermarkComponent;

        public static bool Enabled
        {
            get
            {
                return _watermarkComponent.enabled;
            }
            set
            {
                _watermarkComponent.enabled = value;
            }
        }

        static Watermark()
        {
            // UNSTABLE: GameObject may be removed or renamed in the future!
            //
            _watermarkComponent = GameObject.Find("AlphaVersion").GetComponent<UILabel>();
        }

        public static string Text
        {
            get { return _watermarkComponent.text; }
            set { _watermarkComponent.text = value; }
        }
    }
}
