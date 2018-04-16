using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class AddEventRequest
    {
        public string oauth_token { get; set; }

        public string oauth_token_secret { get; set; }

        public string title { get; set; }

        public string start_time { get; set; }

        public string stop_time { get; set; }

        public string tz_olson_path { get; set; }

        public bool all_day { get; set; }

        public string description { get; set; }

        public int privacy { get; set; }

        public string tags { get; set; }

        public bool free { get; set; }

        public string price { get; set; }

        public string venue_id { get; set; }

        public string parent_id { get; set; }

    }
}