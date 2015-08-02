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

        public static void WriteText(string text, float timePerChar = 0.0753f, int clearDelayUnits = 10, float displayDelay = 0.0f, bool clearOnFinish = true, string timeBarText = "")
        {
            DetectCarObject();

            var wrappedForScreen = text.WordWrap(ScreenColumns);
            for(var i = 1; i <= clearDelayUnits; i++)
            {
                wrappedForScreen += " ";
            }

            _carScreenLogic?.DecodeText(wrappedForScreen, timePerChar, displayDelay, clearOnFinish, timeBarText);
        }

        public static void Clear()
        {
            DetectCarObject();

            _carScreenLogic?.ClearDecodeText();
        }

        public static void Glitch(float intensity, float duration, bool noiseOnly)
        {
            DetectCarObject();

            _carScreenLogic?.GlitchCarScreen(intensity, duration, noiseOnly);
        }

        public static void WriteTimerText(string text, string color, float duration)
        {
            DetectCarObject();

            Color col;
            Color.TryParseHexString(color, out col);

            _carScreenLogic?.timeWidget_?.SetTimeText(
                text, 
                (col == null ? Color.white : col), 
                duration
            );
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
