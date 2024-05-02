using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class OrderResult
    {
        [JsonProperty("customerOrderId")]
        public string CustomerOrderId { get; set; }
        [JsonProperty("orderRef")]
        public string OrderRef { get; set; }
    }
}
