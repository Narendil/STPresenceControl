using System.Threading.Tasks;

namespace STPresenceControl.Contracts
{
    public interface ISerializationService
    {
        Task<T> Deserialize<T>(string source);
        Task<string> Serialize<T>(T source);
    }
}
