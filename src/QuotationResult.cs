using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class QuotationResult
    {
        [JsonProperty("totalFee")]
        public string TotalFee { get; set; }

        [JsonProperty("totalFeeCurrency")]
        public string TotalFeeCurrency { get; set; }
    }
}
