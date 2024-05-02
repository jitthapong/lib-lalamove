using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class DeliveryInfoBuilder
    {
        List<DeliveryInfo> _deliveries;

        public DeliveryInfoBuilder()
        {
            _deliveries = new List<DeliveryInfo>();
        }

        public DeliveryInfoBuilder AddDeliveryInfo(int waypointIndex, Contact toContact, string remark ="")
        {
            _deliveries.Add(
                new DeliveryInfo()
                {
                    ToStop = waypointIndex,
                    ToContact = toContact,
                    Remarks = remark
                });
            return this;
        }

        public List<DeliveryInfo> Deliveries => _deliveries;
    }
}
