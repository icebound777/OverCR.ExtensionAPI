using System;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game
{
    public static class Keyboard
    {
        public static bool GetKeyDown(string key)
        {
            return Input.GetKeyDown(
                ParseKeyCode(key)
            );
        }

        public static bool GetKeyUp(string key)
        {
            return Input.GetKeyUp(
                ParseKeyCode(key)
            );
        }

        public static bool GetKey(string key)
        {
            return Input.GetKey(
                ParseKeyCode(key)
            );
        }

        private static KeyCode ParseKeyCode(string mnemonic)
        {
            return (KeyCode)Enum.Parse(typeof(KeyCode), mnemonic);
        }
    }
}
