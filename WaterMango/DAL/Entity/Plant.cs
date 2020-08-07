using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterMango.DAL.Entity
{
    public class Plant
    {
        [JsonProperty("token")]
        public Guid token { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("location")]
        public string location { get; set; }
        [JsonProperty("wateringHistory")]
        public List<WateringHistory> wateringHistory { get; set; }
        [JsonProperty("isWatering")]
        public bool isWatering { get; set; }
        [JsonProperty("imageUrl")]
        public string imageUrl { get; set; }
    }
}
