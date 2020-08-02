using ImportIOClient.Models;
using ImportIOClient.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ImportIOClient
{
    public class ScheduleClient
    {
        private readonly ImportIOClient _client;
        public const string _baseScheduleUri = "https://schedule.import.io";
        public ScheduleClient(ImportIOClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets all availiable schedules for your extractors
        /// </summary>
        /// <returns>Collection of schedules</returns>
        public async Task<IEnumerable<Schedule>> GetSchedulesAsync()
        {
            return await _client.SendAsync<IEnumerable<Schedule>>(JsonDeserializer.Default
                , new RequestData { Method = HttpMethod.Get }
                , new Field("extractor"));
        }

        /// <summary>
        /// Gets a schedule for particular extractor
        /// </summary>
        /// <param name="extractorId"></param>
        /// <returns>Specific schedule</returns>
        public async Task<Schedule> GetScheduleAsync(string extractorId)
        {
            return await _client.SendAsync<Schedule>(JsonDeserializer.Default
                , new RequestData { Method = HttpMethod.Get }
                , new[]
                {
                    new Field("extractor"),
                    new Field(extractorId)
                });
        }

        /// <summary>
        /// Creates a schedule for your extractor
        /// </summary>
        /// <param name="newSchedule"></param>
        /// <returns>Created schedule</returns>
        public async Task<Schedule> CreateScheduleAsync(Schedule newSchedule)
        {
            return await _client.SendAsync<Schedule>(JsonDeserializer.Default
                , new RequestData
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(JsonConvert.SerializeObject(newSchedule
                    , Formatting.None
                    , new JsonSerializerSettings 
                    {DefaultValueHandling = DefaultValueHandling.Ignore }), Encoding.UTF8, "application/json")
                }
                , new Field("extractor"));
        }

        /// <summary>
        /// Deletes a schedule from your extractor
        /// </summary>
        /// <param name="extractorId"></param>
        /// <returns>Opertaion status</returns>
        public async Task<ImportResult> DeleteScheduleAsync(string extractorId)
        {
            return await _client.SendAsync<ImportResult>(JsonDeserializer.Default, new RequestData
            {
                Method = HttpMethod.Delete
            },
            new[]
            {
                new Field("extractor"),
                new Field(extractorId)
            });
        }
    }
}
