using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class LalamoveConfig
    {
        public string ApiKey { get; set; }
        public string Secret { get; set; }
        public string CountryCode { get; set; } = "TH";
        public string BaseUrl { get; set; }
    }
}
