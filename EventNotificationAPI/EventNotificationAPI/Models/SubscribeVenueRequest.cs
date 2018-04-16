using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class SubscribeVenueRequest : LoginRequest
    {
        public string VenueId { get; set; }

        public string VenueName { get; set; }
    }
}