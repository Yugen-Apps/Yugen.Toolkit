//using System;
//using Yugen.Toolkit.Standard.Core.Helpers;

//namespace Yugen.Toolkit.Uwp.Services
//{
//    public static class SettingsService
//    {
//        static readonly ApplicationDataContainer LocalSettings = ApplicationData.Current.LocalSettings;

//        public static T Load<T>(string settingName)
//        {
//            if (!LocalSettings.Values.ContainsKey(settingName))
//                return default;

//            var value = LocalSettings.Values[settingName];
//            return (T)Convert.ChangeType(value, typeof(T));
//        }

//        public static bool Save(string key, object value)
//        {
//            try
//            {
//                LocalSettings.Values[key] = value;
//                return true;
//            }
//            catch (Exception exception)
//            {
//                LoggerHelper.WriteLine(typeof(SettingsService), exception);
//                return false;
//            }
//        }
//    }
//}
