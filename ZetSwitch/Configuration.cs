using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Microsoft.Win32;

namespace IP_Config
{

   /* public static class Configuration
    {
  /*      static RegistryKey AppKey;
        static string AppName = "IPTwist";
        
        
        static Configuration()
        {
            ConfigurationSettings
            AppKey = Registry.LocalMachine.OpenSubKey("Software\\"+AppName);
            if (AppKey == null)
                AppKey=Registry.LocalMachine.CreateSubKey("Software\\"+AppName);
        }

        public static bool SaveConfigurationOpt(string Name, object Value)
        {
            AppKey.SetValue(Name, Value);
            return true;
        }

        public static string GetStringValue(string Name)
        {
            return AppKey.GetValue(Name);
        }
        public static int GetIntValue(string Name)
        {
            return AppKey.GetValue(Name);
        }
    }*/
}
