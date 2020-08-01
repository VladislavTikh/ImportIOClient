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
            return await _client.SendAsync<IEnumerable<Schedule>>(JsonDeserializer.Default, new Field("extractor"));
        }

        /// <summary>
        /// Gets a schedule for particular extractor
        /// </summary>
        /// <param name="extractorId"></param>
        /// <returns>Specific schedule</returns>
        public async Task<Schedule> GetScheduleAsync(string extractorId)
        {
            return await _client.SendAsync<Schedule>(JsonDeserializer.Default, new[]
            {
                new Field("extractor"),
                new Field(extractorId)
            });
        }

        //public async Task<ImportResult> CreateScheduleAsync(Schedule schedule)
        //{

        //}
    }
}
