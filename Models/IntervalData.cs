using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportIOClient.Models
{
    public class IntervalData
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("minutes")]
        public string Minutes { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
