using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game.Vehicle
{
    public static class FloatingHUD
    {
        private static GameObject _localCarObject;
        private static HoverScreenEmitter _hoverScreenEmitter;

        static FloatingHUD()
        {
            DetectCarObject();
        }

        private static void DetectCarObject()
        {
            if(_localCarObject == null)
                _localCarObject = GameObject.Find("LocalCar");

            if (_hoverScreenEmitter == null)
                _hoverScreenEmitter = _localCarObject?.GetComponent<HoverScreenEmitter>();
        }

        public static void ShowText(float time, int priority, string text, float alphaScale)
        {
            var trickText = new TrickyTextLogic.TrickText(time, priority, TrickyTextLogic.TrickText.TextType.standard, text, alphaScale);
            _hoverScreenEmitter.SetTrickText(trickText);
        }
    }
}
