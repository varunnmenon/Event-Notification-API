using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class SubscribeEventRequest : LoginRequest
    {
        public string EventId { get; set; }

        public string EventName { get; set; }
    }
}