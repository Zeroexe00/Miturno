using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MasterDetail.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string empaque = "Empaque";
        private const string empaquePass = "EmpaquePass";
        private const string remember = "Remember";
        private static readonly string SettingsDefault = string.Empty;
        private static readonly bool Settingsbool = false;

        #endregion


        public static string Empaque
        {
            get
            {
                return AppSettings.GetValueOrDefault(empaque, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(empaque, value);
            }
        }
        public static string EmpaquePass
        {
            get
            {
                return AppSettings.GetValueOrDefault(empaquePass, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(empaquePass, value);
            }
        }
        public static bool Remember
        {
            get
            {
                return AppSettings.GetValueOrDefault(remember, Settingsbool);
            }
            set
            {
                AppSettings.AddOrUpdateValue(remember, value);
            }
        }

    }
}
