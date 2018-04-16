using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class AccessTokenResponse
    {
        public string oauth_token { get; set; }

        public string oauth_token_secret { get; set; }
    }
}