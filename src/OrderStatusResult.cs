using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class OrderStatusResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("price")]
        public Pricing Price { get; set; }
        [JsonProperty("driverId")]
        public string DriverId { get; set; }
        [JsonProperty("customerOrderId")]
        public string CustomerOrderId { get; set; }
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }
    }
}
