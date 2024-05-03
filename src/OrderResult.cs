using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class OrderResult
    {
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        [JsonProperty("quotationId")]
        public string QuotationId { get; set; }
        [JsonProperty("priceBreakdown")]
        public PriceBreakdown PriceBreakdown { get; set; }
        [JsonProperty("driverId")]
        public string DriverId { get; set; }
        [JsonProperty("shareLink")]
        public string ShareLink { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("distance")]
        public Distance Distance { get; set; }
        [JsonProperty("stops")]
        public DeliveryStop[] Stops { get; set; }
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
