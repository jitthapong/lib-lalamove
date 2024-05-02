using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class DriverDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("plateNumber")]
        public string PlateNumber { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
    }
}
