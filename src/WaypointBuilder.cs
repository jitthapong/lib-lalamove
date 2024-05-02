using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lalamove
{
    public class WaypointBuilder
    {
        List<object> _waypoints;

        public WaypointBuilder()
        {
            _waypoints = new List<object>();
        }

        public WaypointBuilder AddWaypoint(string lat, string lng, string address, string countryCode="TH")
        {
            _waypoints.Add(
                new
                {
                    location = new Location()
                    {
                        Lat = lat,
                        Lng = lng
                    },
                    addresses = new
                    {
                        th_TH = new
                        {
                            displayString = address,
                            country = countryCode
                        }
                    }
                });
            return this;
        }

        public WaypointBuilder AddWaypoint(object waypoint)
        {
            _waypoints.Add(waypoint);
            return this;
        }

        public List<object> Waypoints => _waypoints;
    }
}
