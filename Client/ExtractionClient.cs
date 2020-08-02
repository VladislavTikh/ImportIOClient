using ImportIOClient.Models;
using ImportIOClient.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportIOClient.Client
{
    public class ExtractionClient
    {
        private readonly ImportIOClient _client;
        public const string _baseExtractionUri = "https://extraction.import.io";

        public ExtractionClient(ImportIOClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets an extractor by endpoing and Id. Adhoc runs.
        /// </summary>
        /// <param name="extractorId"></param>
        /// <param name="url"></param>
        /// <returns>Specified extractor.</returns>
        public async Task<Extractor> GetExtractorAsync(string extractorId, string url)
        {
            return await _client.SendAsync<Extractor>(JsonDeserializer.Default
                , new RequestData
                {
                    Method = HttpMethod.Get,
                    Query = $"url={url}&"
                }
                , new[]
                {
                    new Field("extractor"),
                    new Field(extractorId)
                });
        }
    }
}
