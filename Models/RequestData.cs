using System.Net.Http;

namespace ImportIOClient.Models
{
    public class RequestData
    {
        public HttpMethod Method { get; set; }
        public StringContent Content { get; set; }
        public string Query { get; set; }
    }
}
