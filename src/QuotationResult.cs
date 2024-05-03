using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class QuotationResult
    {
        [JsonProperty("quotationId")]
        public string QuotationId { get; set; }

        [JsonProperty("scheduleAt")]
        public string ScheduleAt { get; set; }
        [JsonProperty("expiresAt")]
        public string ExpiresAt { get; set; }
        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }
        [JsonProperty("specialRequests")]
        public string[] SpecialRequests { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("priceBreakdown")]
        public PriceBreakdown PriceBreakdown { get; set; }
        [JsonProperty("stops")]
        public DeliveryStop[] Stops { get; set; }
        [JsonProperty("item")]
        public Item Item { get; set; }
        [JsonProperty("distance")]
        public Distance Distance { get; set; }
    }

    public class PriceBreakdown
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        [JsonProperty("specialRequests")]
        public string SpecialRequests { get; set; }
        [JsonProperty("vat")]
        public string Vat { get; set; }
        [JsonProperty("totalBeforeOptimization")]
        public string TotalBeforeOptimization { get; set; }
        [JsonProperty("totalExcludePriorityFee")]
        public string TotalExcludePriorityFee { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public class Distance
    {
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

}
