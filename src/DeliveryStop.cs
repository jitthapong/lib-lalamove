using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class DeliveryStop
    {
        [JsonProperty("stopId")]
        public string StopId { get; set; }
        [JsonProperty("coordinates")]
        public Coordinate Coordinates { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("POD")]
        public POD POD { get; set; }
    }

    public class POD
    {
        public string status { get; set; }
        public string image { get; set; }
        public DateTime deliveredAt { get; set; }
    }
}
