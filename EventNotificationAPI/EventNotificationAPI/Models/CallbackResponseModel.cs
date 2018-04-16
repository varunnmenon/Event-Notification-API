using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class CallbackResponseModel
    {
        public string oauth_token { get; set; }
        public string oauth_verifier { get; set; }
        public string oauth_request_token_secret { get; set; }
    }
}