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

        [JsonProperty("specialRequests")]
        public List<string> SpecialRequests { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; } = "th_TH";
        [JsonProperty("isRouteOptimized")]
        public bool IsRouteOptimized { get; set; } = true;
    }

    public class Item
    {
        public string quantity { get; set; } = "1";
        public string weight { get; set; } = "LESS_THAN_3KG";
        public string[] categories { get; set; } = new string[] { "FOOD_DELIVERY" };
        public string[] handlingInstructions { get; set; } = new string[] { "KEEP_UPRIGHT" };
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

    public class Coordinate
    {
        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }
    }
}
