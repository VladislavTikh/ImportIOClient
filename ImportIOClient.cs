using System;
using System.Net.Http;

namespace ImportIOClient
{
    public class ImportIOClient
    {
        private readonly HttpClient _client;

        private readonly ImportIOConfig _config;

        public ImportIOClient(string apiKey)
            :this(new HttpClient(), apiKey)
        {
        }

        public ImportIOClient(HttpClient client, string apiKey)
        {
            if (client.BaseAddress != null)
            {
                throw new ArgumentException("Base Address must be empty", nameof(HttpClient));
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("API key cannot be empty", nameof(apiKey));
            }
            _client = client;
            _config = new ImportIOConfig
            {
                ApiKey = apiKey
            };
        }

        public void ConfigureClient(Action<ImportIOConfig> configure) => configure?.Invoke(_config);
    }
}