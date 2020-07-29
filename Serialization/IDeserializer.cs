using System.Net.Http;
using System.Threading.Tasks;

namespace ImportIOClient.Serialization
{
    public interface IDeserializer
    {
        Task<T> Deserialize<T>(HttpContent content);
    }
}
