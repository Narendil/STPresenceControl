using STPresenceControl.Contracts;
using System.Threading.Tasks;
using System.Xaml;

namespace STPresenceControl.Libs
{
    public class XamlSerializationService : ISerializationService
    {
        #region ISerializationService

        public Task<T> Deserialize<T>(string source)
        {
            return Task.Run<T>(() => (T)XamlServices.Load(source));
        }

        public Task<string> Serialize<T>(T source)
        {
            return Task.Run<string>(() => XamlServices.Save(source));
        }

        #endregion
    }
}
