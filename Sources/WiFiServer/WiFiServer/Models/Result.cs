using Newtonsoft.Json;

namespace WiFiServer.Models
{
    public class Result
    {
        [JsonProperty("SSID")]
        public string SSSID { get; set; }
        
        [JsonProperty("BSSID")]
        public string BSSID { get; set; }

        [JsonProperty("auth_type")]
        public string AuthType { get; set; }

        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        [JsonProperty("date")]
        public string WiFiDate { get; set; }

        [JsonProperty("RSSI")]
        public string RSSI { get; set; }

    }
}