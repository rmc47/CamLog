using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Engine
{
    public static class RegistryHelper
    {
        private const string c_RegistryRoot = @"SOFTWARE\M0VFC Contest Log";

        public static string GetString(RegistryValue key, string defaultVal)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(c_RegistryRoot, false))
            {
                if (rk == null)
                    return defaultVal;

                return (string)(rk.GetValue(key.ToString(), defaultVal));
            }
        }

        public static int GetInt(RegistryValue key, int defaultVal)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(c_RegistryRoot, false))
            {
                if (rk == null)
                    return defaultVal;

                return (int)(rk.GetValue(key.ToString(), defaultVal));
            }
        }

        public static bool GetBoolean(RegistryValue key, bool defaultVal)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(c_RegistryRoot, false))
            {
                if (rk == null)
                    return defaultVal;

                bool ret;
                if (bool.TryParse((string)(rk.GetValue(key.ToString(), defaultVal)), out ret))
                    return ret;
                else
                    return defaultVal;
            }
        }

        public static void Set(RegistryValue key, string val)
        {
            using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(c_RegistryRoot))
            {
                rk.SetValue(key.ToString(), val);
            }
        }

        public static void Set(RegistryValue key, int val)
        {
            using (RegistryKey rk = Registry.CurrentUser.CreateSubKey(c_RegistryRoot))
            {
                rk.SetValue(key.ToString(), val);
            }
        }

        public static void Set(RegistryValue key, bool val)
        {
            Set(key, val.ToString());
        }
    }

    [System.Reflection.Obfuscation(Exclude=true)]
    public enum RegistryValue
    {
        Server,
        Database,
        Username,
        Password,
        SerialPort,
        CivDtr,
        CivRts,
        Station,
        Locator,
        Operator,
        QrzUsername,
        QrzPassword
    }
}
