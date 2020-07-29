using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportIOClient.Serialization
{
    public class JsonDeserializer : IDeserializer
    {
        private readonly JsonSerializer _serializer = new JsonSerializer();

        //TODO: Get rid of static field
        internal static readonly JsonDeserializer Default = new JsonDeserializer();

        public virtual async Task<T> Deserialize<T>(HttpContent content)
        {
            using (var contentStream = await content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(contentStream))
            {
                using (JsonReader reader = new JsonTextReader(streamReader))
                {
                    return _serializer.Deserialize<T>(reader);
                }
            }
        }
    }
}
