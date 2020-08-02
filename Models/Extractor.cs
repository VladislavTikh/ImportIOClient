using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImportIOClient.Models
{
    public class Extractor
    {
        [JsonProperty("sequenceNumber")]
        public long SequenceNumber { get; set; } 
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }
        [JsonProperty("extractorData")]
        public JObject ExtractorData { get; set; }
        [JsonProperty("exceptionType")]
        public string ExceptionType { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("pageData")]
        public JObject PageData { get; set; }
        [JsonProperty("runtimeConfigId")]
        public string RuntimeConfigId { get; set; }
    }
}
