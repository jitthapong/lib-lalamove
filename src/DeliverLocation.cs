using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class DeliverLocation
    {
        [JsonProperty("location")]
        public Coordinate Location { get; set; }
        [JsonProperty("updateAt")]
        public DateTimeOffset UpdateAt { get; set; }
    }
}
