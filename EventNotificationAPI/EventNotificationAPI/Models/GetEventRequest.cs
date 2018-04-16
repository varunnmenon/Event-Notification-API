using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class GetEventRequest
    {
        public string id { get; set; }
        public string image_sizes { get; set; }
        public string mature { get; set; }
    }
}