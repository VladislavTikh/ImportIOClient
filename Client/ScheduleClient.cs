using ImportIOClient.Models;
using ImportIOClient.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImportIOClient
{
    public class ScheduleClient
    {
        private readonly ImportIOClient _client;
        private const string _scheduleBaseUri = "https://schedule.import.io";
        public ScheduleClient(ImportIOClient client)
        {
            _client = client;
            _client.ConfigureClient(x => x.BaseUri = new Uri(_scheduleBaseUri));
        }

        public async Task<IEnumerable<Extractor>> GetExtractorsAsync()
        {
            return await _client.SendAsync<IEnumerable<Extractor>>(JsonDeserializer.Default);
        }
    }
}
