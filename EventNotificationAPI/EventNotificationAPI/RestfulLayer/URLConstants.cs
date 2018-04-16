using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventNotificationAPI.RestfulLayer
{
    public class URLConstants
    {
        //Eventful API Constants
        public static string APIKey = "4ncqBGC6kTdhkzmp";
        public static string SearchEventsByLocation = "http://api.eventful.com/rest/events/search?app_key="+APIKey+"&";
        public static string EventDetailsURL = "http://api.eventful.com/rest/events/get?app_key=" + APIKey + "&";
        public static string VenueDetailsURL = "http://api.eventful.com/rest/venues/get?app_key=" + APIKey + "&";
        public static string SearchVenuesByLocation = "http://api.eventful.com/rest/venues/search?app_key=" + APIKey + "&";
        public static string AddEventToVenueURL = "http://api.eventful.com/rest/events/new";
        public static string APIconsumerKey = "563babe4317c36fd9a26";
        public static string APISecretKey = "0640130c8c6ae6c3bc4d";
        public static string callbackURL = "http://eventnotificationapi20180319063355.azurewebsites.net/api/Eventful/GetAuthTokenCallback";
        public static string EventfulBaseURI = "http://eventful.com/";
        public static string requestTokenBaseURI = "http://eventful.com/oauth/request_token";
        public static string AuthorizeTokenBaseURI = "http://eventful.com/oauth/authorize?oauth_token=";
        public static string AccessTokenBaseURI = "http://eventful.com/oauth/access_token";
        public static string GetCategoriesURL = "http://api.eventful.com/rest/categories/list?app_key=" + APIKey + "&subcategories=1&aliases=1";
        //Firebase API Constants
        public static string FirebaseServerKey = "AIzaSyBSsK0aQPyIxWCxr18ullm_JV4T5eAZdyw";
        public static string FirebaseSenderId = "346790094968";
        public static string FirebaseDbURL = "https://eventnotificationtest-4c820.firebaseio.com/";
        public static string FirebaseLoginNode = "LoginDetails";
        public static string FirebaseEventNode = "SubscribeEvent";
        public static string FirebaseVenueNode = "SubscribeVenue";
        public static string FirebaseSubscribeTopic = "https://iid.googleapis.com/iid/v1/";
        public static string FirebaseUnSubscribeTopic = "https://iid.googleapis.com/iid/v1:batchRemove";
        public static string FirebaseSendNotifictionURL = "https://fcm.googleapis.com/fcm/send";
        public static string FirebaseActivityName = "EventDescription";
    }
}