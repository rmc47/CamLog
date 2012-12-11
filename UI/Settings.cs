using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace UI
{
    internal static class Settings
    {
        private const string c_Key = @"SOFTWARE\M0VFC\CamLog";

        public static string Get(string key, string defaultValue)
        {
            using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(c_Key, false))
            {
                if (regKey == null)
                    return defaultValue;
                return (string)regKey.GetValue(key, defaultValue);
            }
        }

        public static void Set(string key, string val)
        {
            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(c_Key))
            {
                regKey.SetValue(key, val);
            }
        }
    }
}
