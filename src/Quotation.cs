using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Lalamove
{
    public class Quotation
    {
        [JsonProperty("scheduleAt")]
        public string ScheduleAt { get; set; }

        [JsonProperty("serviceType")]
        public string ServiceType { get; set; }

        [JsonProperty("stops")]
        public List<object> Stops { get; set; }

        [JsonProperty("deliveries")]
        public List<DeliveryInfo> Deliveries { get; set; }

        [JsonProperty("requesterContact")]
        public Contact RequesterContact { get; set; }

        [JsonProperty("specialRequests")]
        public List<string> SpecialRequests { get; set; }

        [JsonProperty("quotedTotalFee")]
        public Pricing QuotedTotalFee { get; set; }

        [JsonProperty("sms")]
        public bool SendSms { get; set; }
    }

    public class DeliveryInfo
    {
        [JsonProperty("toStop")]
        public int ToStop { get; set; }

        [JsonProperty("toContact")]
        public Contact ToContact { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }

    public class Contact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }
    }
}
