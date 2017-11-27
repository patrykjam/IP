using System.Collections.Generic;
using Newtonsoft.Json;

namespace WiFiServer.Models
{
    public class JsonData
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("location")]
        public Location LocationData { get; set; }

        [JsonProperty("results")]
        public List<Result> ResultsData { get; set; }
    }
}