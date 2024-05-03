using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class DeliveryDetail
    {
        public DeliveryDetail(string lat, string lng, string addressDetail, string contactName, string phoneNumber)
        {
            Lat = lat;
            Lng = lng;
            ContactName = contactName;
            PhoneNumber = phoneNumber;
            AddressDetail = addressDetail;

            if (!PhoneNumber.StartsWith("+66"))
                PhoneNumber = $"+66{PhoneNumber}";
        }

        public string Lat { get; set; }
        public string Lng { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDetail { get; set; }
    }
}
