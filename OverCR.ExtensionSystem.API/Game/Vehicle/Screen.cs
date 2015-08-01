using System;
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

        public static void WriteText(string text, float speed = 0.10f, int clearDelayUnits = 10, float displayDelay = 0.0f, bool clearOnFinish = true, string timeBarText = "")
        {
            DetectCarObject();

            if (!CarScreenPresent())
                return;

            var wrappedForScreen = Regex.Replace(text, $"\\w.{{{ScreenColumns}}}", "$0\n");

            for(var i = 1; i <= clearDelayUnits; i++)
            {
                wrappedForScreen += " ";
            }

            _carScreenLogic.DecodeText(wrappedForScreen, speed, displayDelay, clearOnFinish, timeBarText);
        }

        public static void Clear()
        {
            DetectCarObject();
            if (!CarScreenPresent())
                return;

            _carScreenLogic.ClearDecodeText();
        }

        private static void DetectCarObject()
        {
            if (_localScreenGroupObject == null)
                _localScreenGroupObject = GameObject.Find("CarScreenGroup");

            if (_carScreenLogic == null)
                _carScreenLogic = _localScreenGroupObject?.GetComponent<CarScreenLogic>();
        }

        private static bool CarScreenPresent()
        {
            return (_carScreenLogic != null && _localScreenGroupObject != null);
        }
    }
}
