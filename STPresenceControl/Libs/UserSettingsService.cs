using STPresenceControl.Contracts;
using System.Threading.Tasks;

namespace STPresenceControl.Libs
{
    public class UserSettingsService : ISettingsService
    {
        #region Fields

        private ISerializationService _serializationService;

        #endregion

        #region Ctor

        public UserSettingsService(ISerializationService serializationService)
        {
            _serializationService = serializationService;
        }

        #endregion

        #region ISettingsService

        public async Task<T> GetSettingAsync<T>(string key)
        {
            var serializedValue = LoadSetting(key);
            return await _serializationService.Deserialize<T>(serializedValue);
        }

        public async Task SetSettingsAsync<T>(string key, T value)
        {
            var serializedValue = await _serializationService.Serialize(value);
            SaveSetting(key, serializedValue);
        }

        #endregion

        #region Private

        private void SaveSetting(string key, string value)
        {
            Properties.Settings.Default[key] = value;
            Properties.Settings.Default.Save();
        }

        private string LoadSetting(string key)
        {
            var value = Properties.Settings.Default[key];
            return (value != null ? value.ToString() : string.Empty);
        }

        #endregion
    }
}
