using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Lalamove
{
    public class QuotationBuilder
    {
        Quotation _quotation;

        public QuotationBuilder()
        {
            _quotation = new Quotation();
        }

        public QuotationBuilder SetSchedule(DateTime date)
        {
            _quotation.ScheduleAt = date.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ff'Z'", CultureInfo.InvariantCulture);
            return this;
        }

        public QuotationBuilder SetServiceType(string serviceType = "MOTORCYCLE")
        {
            _quotation.ServiceType = serviceType;
            return this;
        }

        public QuotationBuilder SetDeliveryInfo(DeliveryDetail sender, DeliveryDetail recipient)
        {
            SetDeliveryInfo(sender, new DeliveryDetail[] { recipient });
            return this;
        }

        public QuotationBuilder SetDeliveryInfo(DeliveryDetail sender, DeliveryDetail[] recipients)
        {
            var wayPointbuilder = new WaypointBuilder()
               .AddWaypoint(sender.Lat, sender.Lng, sender.AddressDetail);

            var deliveryBuilder = new DeliveryInfoBuilder();

            for (var i = 0; i < recipients.Count(); i++)
            {
                var recipient = recipients[i];
                wayPointbuilder.AddWaypoint(recipient.Lat, recipient.Lng, recipient.AddressDetail);

                deliveryBuilder.AddDeliveryInfo(i + 1, new Contact() { Name = recipient.ContactName, Phone = recipient.PhoneNumber });
            }

            var stops = wayPointbuilder.Waypoints;
            _quotation.Stops ??= new List<object>();
            _quotation.Stops.AddRange(stops);

            _quotation.Deliveries = deliveryBuilder.Deliveries;

            _quotation.RequesterContact = new Contact()
            {
                Name = sender.ContactName,
                Phone = sender.PhoneNumber
            };

            return this;
        }

        /// <summary>
        /// "COD", "HELP_BUY", "LALABAG"
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public QuotationBuilder SetSpecialRequest(List<string> requests)
        {
            _quotation.SpecialRequests = new List<string>();
            _quotation.SpecialRequests.AddRange(requests);
            return this;
        }

        public QuotationBuilder SetQuotedTotalFee(QuotationResult result)
        {
            _quotation.QuotedTotalFee = new Pricing()
            {
                Amount = result.TotalFee,
                Currency = result.TotalFeeCurrency
            };
            return this;
        }

        public Quotation Quotation => _quotation;
    }
}
