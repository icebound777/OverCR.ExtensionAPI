using System;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game
{
    public static class Keyboard
    {
        public static bool GetKeyDown(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            try
            {
                return Input.GetKeyDown(
                    ParseKeyCode(key)
                );
            }
            catch
            {
                return false;
            }
        }

        public static bool GetKeyUp(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            try
            {
                return Input.GetKeyUp(
                    ParseKeyCode(key)
                );
            }
            catch
            {
                return false;
            }
        }

        public static bool GetKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return false;

            try
            {
                return Input.GetKey(
                    ParseKeyCode(key)
                );
            }
            catch
            {
                return false;
            }

        }

        private static KeyCode ParseKeyCode(string mnemonic)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), mnemonic);
        }
    }
}
