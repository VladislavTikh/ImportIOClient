using System;
using System.Threading.Tasks;

namespace ImportIOClient
{
    public class DataClient
    {
        private readonly ImportIOClient _client;
        private const string _baseDataUri = "https://data.import.io";

        internal DataClient(ImportIOClient client)
        {
            _client = client;
            _client.ConfigureClient(x => x.BaseUri = new Uri(_baseDataUri));
        }

        /// <summary>
        /// Get the latest crawl run results in desired format.
        /// </summary>
        /// <param name="extractorID">Unique identifier of your extractor.</param>
        /// <param name="format">Specifies returned format.</param>
        /// <returns></returns>
        public async Task<string> GetLatestRunResultAsync(string extractorID, ContentFormat format = ContentFormat.JSON)
        {
            if (string.IsNullOrEmpty(extractorID))
            {
                throw new ArgumentException("Extractor ID cannot be empty");
            }
            return await _client.GetRawDataAsync(new[]
            {
              new Field("extractor"),
              new Field(extractorID),
              new Field(format.ToString().ToLower()),
              new Field("latest")
            });
        }
    }

    public enum ContentFormat
    {
        JSON,
        CSV
    }
}
