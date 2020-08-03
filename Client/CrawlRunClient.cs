using ImportIOClient.Models;
using ImportIOClient.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportIOClient.Client
{
    public class CrawlRunClient
    {
        private readonly ImportIOClient _client;
        public const string _baseCrawlRunUri = "https://run.import.io";

        public CrawlRunClient(ImportIOClient client)
        {
            _client = client;
        }

        public async Task<CrawlRun> StartExtractorCrawl(string extractorId)
        {
            return await _client.SendAsync<CrawlRun>(JsonDeserializer.Default
                , new RequestData
                {
                    Method = HttpMethod.Post,
                }
                , new[]
                {
                    new Field("extractor"),
                    new Field(extractorId),
                    new Field("start")
                });
        }

        public async Task<CrawlRun> CancelExtractorCrawl(string extractorId)
        {
            return await _client.SendAsync<CrawlRun>(JsonDeserializer.Default
                , new RequestData
                {
                    Method = HttpMethod.Post,
                }
                , new[]
                {
                    new Field("extractor"),
                    new Field(extractorId),
                    new Field("cancel")
                });
        }
    }
}
