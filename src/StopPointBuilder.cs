using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class StopPointBuilder
    {
        List<DeliveryStop> _stopPoints;

        public StopPointBuilder()
        {
            _stopPoints = new List<DeliveryStop>();
        }

        public StopPointBuilder AddStopPoint(string lat, string lng, string address)
        {
            _stopPoints.Add(
                new DeliveryStop
                {
                    Coordinates = new Coordinate()
                    {
                        Lat = lat,
                        Lng = lng
                    },
                    Address = address
                });
            return this;
        }

        public StopPointBuilder AddStopPoint(DeliveryStop stopPoint)
        {
            _stopPoints.Add(stopPoint);
            return this;
        }

        public List<DeliveryStop> StopPoints => _stopPoints;
    }
}
