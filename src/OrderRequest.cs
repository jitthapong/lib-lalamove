using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class OrderRequest
    {
        public string quotationId { get; set; }
        public Sender sender { get; set; }
        public Recipient[] recipients { get; set; }
        public bool isPODEnabled { get; set; }
        public string partner { get; set; }
        public Metadata metadata { get; set; }
    }
    public class Sender
    {
        public string stopId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class Metadata
    {
        public string restaurantOrderId { get; set; }
        public string restaurantName { get; set; }
    }

    public class Recipient
    {
        public string stopId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string remarks { get; set; }
    }
}
