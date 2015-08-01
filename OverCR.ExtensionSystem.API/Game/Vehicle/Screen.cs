using System.Text.RegularExpressions;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.Vehicle
{
    public static class Screen
    {
        public static int ScreenColumns { get; } = 20;

        private static GameObject _localScreenGroupObject;
        private static CarScreenLogic _carScreenLogic;

        static Screen()
        {
            DetectCarObject();
        }

        public static void WriteText(string text, float speed = 1.0f, float delay = 0.0f, bool clearOnFinish = true, string timeBarText = "")
        {
            DetectCarObject();

            var wrappedForScreen = Regex.Replace(text, $"\\w.{{{ScreenColumns}}}", "$0\n");
            _carScreenLogic.DecodeText(wrappedForScreen, speed, delay, clearOnFinish, timeBarText);
        }

        public static void Clear()
        {
            DetectCarObject();

            _carScreenLogic.ClearDecodeText();
        }

        private static void DetectCarObject()
        {
            if (_localScreenGroupObject == null)
                _localScreenGroupObject = GameObject.Find("CarScreenGroup");

            if (_carScreenLogic == null)
                _carScreenLogic = _localScreenGroupObject?.GetComponent<CarScreenLogic>();
        }
    }
}
