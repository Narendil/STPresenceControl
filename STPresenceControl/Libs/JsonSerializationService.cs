using Newtonsoft.Json;
using STPresenceControl.Contracts;
using System.Threading.Tasks;

namespace STPresenceControl.Libs
{
    public class JsonSerializationService : ISerializationService
    {
        #region ISerializationService

        public Task<T> Deserialize<T>(string source)
        {
            return Task.Run(() =>
                JsonConvert.DeserializeObject<T>(source));
        }

        public Task<string> Serialize<T>(T source)
        {
            return Task.Run(() =>
                JsonConvert.SerializeObject(source));
        }

        #endregion
    }
}
