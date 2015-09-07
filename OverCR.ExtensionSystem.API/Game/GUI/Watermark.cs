using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.GUI
{
    public static class Watermark
    {
        private static readonly UILabel WatermarkComponent;

        public static bool Enabled
        {
            get
            {
                return WatermarkComponent.enabled;
            }
            set
            {
                WatermarkComponent.enabled = value;
            }
        }

        static Watermark()
        {
            // UNSTABLE: GameObject may be removed or renamed in the future!
            //
            WatermarkComponent = GameObject.Find("AlphaVersion").GetComponent<UILabel>();
        }

        public static string Text
        {
            get { return WatermarkComponent.text; }
            set { WatermarkComponent.text = value; }
        }
    }
}
