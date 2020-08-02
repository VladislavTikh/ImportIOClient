using ImportIOClient.Client;
using ImportIOClient.Models;
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
        private DataClient _dataClient;
        private ScheduleClient _scheduleClient;
        private ExtractionClient _extractionClient;

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

        public void ConfigureClient(Action<ImportIOConfig> configure) => configure?.Invoke(_config);

        public DataClient Data
        {
            get
            {
                ConfigureClient(x => x.BaseUri = new Uri(DataClient._baseDataUri));
                if (_dataClient == null)
                {
                    _dataClient = new DataClient(this);
                }
                return _dataClient;
            }
        }

        public ScheduleClient Schedule
        {
            get
            {
                ConfigureClient(x => x.BaseUri = new Uri(ScheduleClient._baseScheduleUri));
                if (_scheduleClient == null)
                {
                    _scheduleClient = new ScheduleClient(this);
                }
                return _scheduleClient;
            }
        }

        public ExtractionClient Extraction
        {
            get
            {
                ConfigureClient(x => x.BaseUri = new Uri(ExtractionClient._baseExtractionUri));
                if (_extractionClient == null)
                {
                    _extractionClient = new ExtractionClient(this);
                }
                return _extractionClient;
            }
        }

        public Task<string> GetRawDataAsync(params Field[] fields)
        {
            return SendAsync(fields, content => content.ReadAsStringAsync(), new RequestData
            {
                Method = HttpMethod.Get
            });
        }

        internal Task<T> SendAsync<T>(IDeserializer deserialiser, RequestData data, params Field[] fields)
        {
            return SendAsync(fields, deserialiser.Deserialize<T>, data);
        }

        private async Task<T> SendAsync<T>(Field[] fields, Func<HttpContent, Task<T>> deserialise, RequestData data)
        {
            var parameters = CreateParameters(fields, data.Query);
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_config.BaseUri, $"/{parameters}"),
                Method = data.Method,
                Content = data.Content
            };
            using (var responseMessage = await _client.SendAsync(request))
            {
                if (!responseMessage.IsSuccessStatusCode)
                    throw new Exception(responseMessage.ReasonPhrase);
                return await deserialise(responseMessage.Content);
            }
        }

        private string CreateParameters(Field[] fields, string query)
        {
            if (string.IsNullOrEmpty(_config.ApiKey))
            {
                throw new InvalidOperationException("API key not set");
            }
            var parameters = string.Join("/", fields.Select(x => x.Value));
            return $"{parameters}?{query}_apikey={_config.ApiKey}";
        }
    }
}