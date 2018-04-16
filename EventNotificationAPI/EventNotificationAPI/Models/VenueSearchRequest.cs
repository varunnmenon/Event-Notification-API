using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class VenueSearchRequest
    {
        public string keywords { get; set; }

        public string location { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public bool count_only { get; set; }

        public int page_size { get; set; }

        public int page_number { get; set; }

        public int within { get; set; }

        public string units { get; set; }

        public string sort_order { get; set; }

        public string sort_direction { get; set; }

    }
}