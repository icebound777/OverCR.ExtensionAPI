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

        public static void WriteText(float time, int priority, string text, float alphaScale)
        {
            DetectCarObject();

            var trickText = new TrickyTextLogic.TrickText(time, priority, TrickyTextLogic.TrickText.TextType.standard, text, alphaScale);
            _hoverScreenEmitter?.SetTrickText(trickText);
        }

        public static void Show()
        {
            DetectCarObject();

            if(_hoverScreenEmitter != null && !_hoverScreenEmitter.enabled)
                _hoverScreenEmitter.enabled = true;
        }

        public static void Hide()
        {
            DetectCarObject();

            if (_hoverScreenEmitter != null && _hoverScreenEmitter.enabled)
                _hoverScreenEmitter.enabled = false;
        }
    }
}
