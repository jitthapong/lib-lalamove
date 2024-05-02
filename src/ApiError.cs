using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class ApiError
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
