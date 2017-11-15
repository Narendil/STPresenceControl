using System.Configuration;

namespace STPresenceControl.Common
{
    public class SettingsService
    {
        #region Ctor

        private SettingsService()
        {

        }

        #endregion

        #region Public

        private static SettingsService _instance;
        public static SettingsService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingsService();
                return _instance;
            }
        }

        public void SaveSetting(string key, string value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }

        public string LoadSetting(string key)
        {
            var value = Properties.Settings.Default[key];
            return (value != null ? value.ToString() : string.Empty);
        }

        #endregion

    }
}
