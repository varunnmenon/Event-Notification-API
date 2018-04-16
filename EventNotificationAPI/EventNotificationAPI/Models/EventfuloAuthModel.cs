using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class EventfuloAuthModel : AccessTokenResponse
    {
        public string oauth_callback_confirmed { get; set; }
        public string oauth_verifier { get; set; }
    }
}