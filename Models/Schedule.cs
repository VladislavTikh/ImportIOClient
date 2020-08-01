using Newtonsoft.Json;

namespace ImportIOClient.Models
{
    public class Schedule
    {
        [JsonProperty("extractorId")]
        public string ExtractorId { get; set; }
        [JsonProperty("ownderId")]
        public string OwnerId { get; set; }
        [JsonProperty("intervalData")]
        public IntervalData IntervalData { get; set; }
        [JsonProperty("interval")]
        public string Interval { get; set; }
        [JsonProperty("nextRunAt")]
        public decimal NextRunAt { get; set; }
        [JsonProperty("startTimestamp")]
        public string StartTimeStamp { get; set; }
    }
}
