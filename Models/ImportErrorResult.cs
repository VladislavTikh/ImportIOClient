using Newtonsoft.Json;

namespace ImportIOClient.Models
{
    public class ImportErrorResult
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
