using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterMango.Model
{
    public class wateringInfoModel
    {
        [JsonProperty("plantToken")]
        public Guid plantToken { get; set; }

        [JsonProperty("isWatering")]
        public bool isWatering { get; set; }
        [JsonProperty("duration")]
        public int? duration { get; set; }
    }
}
