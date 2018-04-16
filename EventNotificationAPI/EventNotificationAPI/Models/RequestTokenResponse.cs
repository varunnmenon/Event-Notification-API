using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class RequestTokenResponse
    {
        public string URL { get; set; }

        public string RequestTokenSecret { get; set; }
    }
}