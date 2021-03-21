using Newtonsoft.Json;

namespace eka
{
    public struct configjson
    {
        [JsonProperty("token")]
        public string token { get; private set; }
        [JsonProperty("prefix")]
        public string prefix {get; private set;}
    }
}
