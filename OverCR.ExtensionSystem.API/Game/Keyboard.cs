using System;
using System.Collections.Generic;
using UnityEngine;

namespace OverCR.ExtensionSystem.API.Game
{
    public static class Keyboard
    {
        private static readonly List<string> FailedKeys;

        static Keyboard()
        {
            FailedKeys = new List<string>();
        }

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
            catch (Exception ex) when (!FailedKeys.Contains(key))
            {
                FailedKeys.Add(key);
                Console.WriteLine($"Error occured when processing keycode '{key}': {ex}");

                return false;
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
            catch (Exception ex) when (!FailedKeys.Contains(key))
            {
                FailedKeys.Add(key);
                Console.WriteLine($"Error occured when processing keycode '{key}': {ex}");

                return false;
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
            catch (Exception ex) when (!FailedKeys.Contains(key))
            {
                FailedKeys.Add(key);
                Console.WriteLine($"Error occured when processing keycode '{key}': {ex}");

                return false;
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
