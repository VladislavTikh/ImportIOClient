using Newtonsoft.Json;

namespace ImportIOClient.Models
{
    public class CrawlRun
    {
        [JsonProperty("crawlRunId")]
        public string Id { get; set; }
    }
}
