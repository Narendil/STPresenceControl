using System.Threading.Tasks;

namespace STPresenceControl.Contracts
{
    public interface ISettingsService
    {
        Task<T> GetSettingAsync<T>(string key);
        Task SetSettingsAsync<T>(string key, T value);
    }
}
