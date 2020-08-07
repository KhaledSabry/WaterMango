using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterMango.DAL.Entity
{
    public class WateringHistory
    {
        [JsonProperty("token")]
        public Guid token { get; set; }
        [JsonProperty("time")]
        public DateTime time { get; set; }
        [JsonProperty("duration")]
        public int duration { get; set; }
    }
}
