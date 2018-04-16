using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.Models
{
    public class EventSearchRequest
    {
        public string keywords { get; set; }

        public string location { get; set; }

        public string latitude { get; set; }

        public string longitude { get; set; }

        public string date { get; set; }

        public string category { get; set; }

        public string ex_category { get; set; }

        public int within { get; set; }

        public string units { get; set; }

        public bool count_only { get; set; }

        public string sort_order { get; set; }

        public string sort_direction { get; set; }

        public int page_size { get; set; }

        public int page_number { get; set; }

        public string image_sizes { get; set; }

        public int languages { get; set; }

        public string mature { get; set; }

        public string include { get; set; }

        public bool change_multi_day_start { get; set; }
    }
}