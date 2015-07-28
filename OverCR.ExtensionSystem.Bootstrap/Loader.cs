﻿using System;
using System.Reflection;
using UnityEngine;
using static System.IO.File;

namespace OverCR.ExtensionSystem.Bootstrap
{
    public static class Loader
    {
        public static object ExtensionManager;

        public static void BootExtensionManager()
        {
            var str = $"{Application.dataPath}/ExtensionSystem/OverCR.ExtensionSystem.Manager.dll";

            if (!Exists(str))
                return;

            try {
                foreach (Type type in Assembly.LoadFrom(str).GetTypes())
                {
                    if (type.FullName == "OverCR.ExtensionSystem.Manager.ExtensionManager")
                    {
                        ExtensionManager = type.GetProperty(
                            "Instance",
                            BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy
                        ).GetValue(null, null);
                    }
                    break;
                }
            } catch(Exception ex)
            {
                Console.WriteLine("OverCR: Can't load ExtensionSystem.Manager. No extensibility will be available.");
                Console.WriteLine(ex);
            }
        }

        public static void UpdateExtensionManager()
        {
            try
            {
                ExtensionManager?.GetType().GetMethod("UpdateExtensions").Invoke(ExtensionManager, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine("OverCR: Can't update extensions");
            }
        }
    }
}
