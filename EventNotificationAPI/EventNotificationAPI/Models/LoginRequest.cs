using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class LoginRequest
    {
        public string EmailId { get; set; }

        public string InstanceId { get; set; }
    }
}