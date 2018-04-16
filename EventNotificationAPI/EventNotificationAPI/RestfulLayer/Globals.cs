using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.RestfulLayer
{
    public static class Globals
    {
        public enum eventdetails
        {
            FromVenue,
            FromEvent
        }

        public enum eventNotification
        {
            AddEvent,
            EditEvent
        }
        //public static string username = "varunMiddleware";
        //public static string password = "Billion@12";
    }
}