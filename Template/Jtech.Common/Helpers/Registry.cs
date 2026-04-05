using win=Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Jtech.Common.Helpers
{
    public enum RootRegistry
    { 
        LocalMachine,
        User,
        ClassRoot,
        CurrentUser,
        CurrentConfig
    }

    public static class Registry
    {

        internal static win.RegistryKey? GetRootRegistry(RootRegistry root)
        {
            switch (root)
            {
                case RootRegistry.LocalMachine:
                    return win.Registry.LocalMachine;
                case RootRegistry.User:
                    return win.Registry.Users;
                case RootRegistry.CurrentUser:
                    return win.Registry.CurrentUser;
                case RootRegistry.ClassRoot:
                    return win.Registry.ClassesRoot;
                case RootRegistry.CurrentConfig:
                    return win.Registry.CurrentConfig;
                default:return null;
            }
        }

        public static object? GetValue(RootRegistry root,string subKey, string KeyName)
        {
            var rootRegis = GetRootRegistry(root);
            if (rootRegis == null) 
                return null;

            using (win.RegistryKey key = rootRegis.OpenSubKey(subKey))
            {
                return key.GetValue(KeyName);
            }
        }

        public static JObject GetValues(RootRegistry root, string subKey)
        {
            var rootRegis = GetRootRegistry(root);
            if (rootRegis == null)
                return null;

            JObject obj = new JObject();
            using (win.RegistryKey key = rootRegis.OpenSubKey(subKey))
            {
                foreach (string subkey in key.GetSubKeyNames())
                    obj.Add(subkey, JToken.FromObject(key.GetValue(subkey)));
                return obj;
            }
        }

        public static void SetValue(RootRegistry root, string subKey,string keyName,object value)
        {
            var rootRegis = GetRootRegistry(root);
            if (rootRegis == null)
                return;

            win.RegistryKey? regisKey = null;

            if (!rootRegis.GetSubKeyNames().Contains(subKey))
                regisKey = rootRegis.CreateSubKey(subKey);
            else
                regisKey = rootRegis.OpenSubKey(subKey);

            regisKey.SetValue(keyName, value);
            regisKey.Dispose();
        }
    }
}
