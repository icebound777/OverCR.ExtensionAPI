using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.Vehicle
{
    public static class Screen
    {
        private static GameObject _localScreenGroupObject;
        private static CarScreenLogic _carScreenLogic;

        static Screen()
        {
            DetectCarObject();
        }

        public static void WriteText(string text, float speed = 1.0f, float delay = 0.0f, bool clearOnFinish = true, string timeBarText = "")
        {
            _carScreenLogic.DecodeText(text, speed, delay, clearOnFinish, timeBarText);
        }

        public static void Clear()
        {
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
