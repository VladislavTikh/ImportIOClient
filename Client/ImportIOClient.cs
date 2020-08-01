using ImportIOClient.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImportIOClient
{
    public class ImportIOClient
    {
        private readonly HttpClient _client;

        private readonly ImportIOConfig _config;
        private readonly DataClient _dataClient;
        private readonly ScheduleClient scheduleClient;

        public ImportIOClient(string apiKey)
            : this(new HttpClient(), apiKey)
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

        public DataClient Data
        {
            get
            {
                ConfigureClient(x => x.BaseUri = new Uri(DataClient._baseDataUri));
                return _dataClient ?? new DataClient(this);
            }
        }

        public ScheduleClient Schedule
        {
            get
            {
                ConfigureClient(x => x.BaseUri = new Uri(ScheduleClient._baseScheduleUri));
                return scheduleClient ?? new ScheduleClient(this);
            }
        }

        public void ConfigureClient(Action<ImportIOConfig> configure) => configure?.Invoke(_config);

        public Task<string> GetRawDataAsync(params Field[] fields)
        {
            return SendAsync(fields, content => content.ReadAsStringAsync());
        }

        internal Task<T> SendAsync<T>(IDeserializer deserialiser, params Field[] fields)
        {
            return SendAsync(fields, deserialiser.Deserialize<T>);
        }

        private async Task<T> SendAsync<T>(Field[] fields, Func<HttpContent, Task<T>> deserialise)
        {
            var parameters = CreateParameters(fields);
            var uri = new Uri(_config.BaseUri, $"/{parameters}");
            using (var responseMessage = await _client.GetAsync(uri))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new Exception("Error");
                return await deserialise(responseMessage.Content);
            }
        }

        private string CreateParameters(Field[] fields)
        {
            if (string.IsNullOrEmpty(_config.ApiKey))
            {
                throw new InvalidOperationException("API key not set");
            }
            var parameters = string.Join("/", fields.Select(x => x.Value));
            return $"{parameters}?_apikey={_config.ApiKey}";
        }
    }
}