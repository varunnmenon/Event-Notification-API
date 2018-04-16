using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventNotificationAPI.Models;
using EventNotificationAPI.RestfulLayer;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace EventNotificationAPI.DataAccessLayer
{
    public class Event
    {
        public static FirebaseDB firebaseDBEvent = new FirebaseDB(URLConstants.FirebaseDbURL).Node(URLConstants.FirebaseEventNode);

        public bool SubscribeEvent(SubscribeEventRequest request)
        {
            try
            {
                string getResponseJSON = "";
                bool isExist = false;
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBEvent.Get("?orderBy=\"EventId\"&equalTo=\"" + request.EventId + "\"");
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;
                        foreach (var result in jObj)
                        {
                            Console.WriteLine(result.Key);
                            getResponseJSON = (string)result.Value["EventId"];

                            if (getResponseJSON == request.EventId)
                            {
                                FirebaseResponse patchInstanceId = firebaseDBEvent
                                    .NodePath(result.Key + "/InstanceId")
                                    .Patch("{\"" + request.InstanceId + "\":\"true\"}");
                                Console.WriteLine(patchInstanceId.Success);
                                if (patchInstanceId.Success)
                                {
                                    isExist = true;
                                    return true;
                                }
                            }
                        }
                    }
                }

                if (!isExist)
                {
                    jObj = null;
                    count = 0;
                    getResponse = firebaseDBEvent.Get();
                    if (getResponse.Success)
                    {
                        if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                        {
                            getResponseJSON = getResponse.JSONContent;
                            jObj = JObject.Parse(getResponseJSON);
                            count = jObj.Count;
                        }
                    }
                    Console.WriteLine("Patch Login Request");
                    FirebaseResponse patchRequest = firebaseDBEvent.Patch("{\"E" + (count + 1) + "\":{\"EventId\":\"" + request.EventId + "\",\"EventName\":\"" + request.EventName + "\",\"InstanceId\":{\"" + request.InstanceId + "\":\"true\"}}}");
                    Console.WriteLine(patchRequest.Success);
                    if (patchRequest.Success)
                        return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
        }

        public bool UnsubscribeEvent(SubscribeEventRequest request)
        {
            try
            {
                string getResponseJSON = "";
                FirebaseResponse getResponse = firebaseDBEvent.Get("?orderBy=\"EventId\"&equalTo=\"" + request.EventId + "\"");
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        JObject jObj = JObject.Parse(getResponseJSON);
                        foreach (var result in jObj)
                        {
                            Console.WriteLine(result.Key);
                            getResponseJSON = result.Key;

                            if ((string)result.Value["EventId"] == request.EventId)
                            {
                                Console.WriteLine("DELETE Request");
                                FirebaseResponse deleteResponse = firebaseDBEvent
                                    .NodePath(getResponseJSON + "/InstanceId/" + request.InstanceId).Delete();

                                Console.WriteLine(deleteResponse.Success);
                                if (deleteResponse.Success)
                                    return true;

                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
        }

        public string GetSubscribedEvent(LoginRequest request)
        {
            string response = "";
            try
            {
                string getResponseJSON = "";
                IList<SubscribeEventRequest> eventList = new List<SubscribeEventRequest>();
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBEvent.Get();
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;
                        foreach (var result in jObj)
                        {
                            SubscribeEventRequest subscribeRequest = null;
                            Console.WriteLine(result.Key);
                            getResponseJSON = result.Value["InstanceId"].ToString();

                            if (!getResponseJSON.ToString().Equals("") && !getResponseJSON.ToString().Equals("null") && !getResponseJSON.ToString().Equals("{}"))
                            {
                                JObject instanceJson = JObject.Parse(getResponseJSON);
                                Console.WriteLine(getResponseJSON);
                                foreach (var instances in instanceJson)
                                {
                                    Console.WriteLine(instances.Key);
                                    if (!instances.Key.Equals("") && (instances.Key == request.InstanceId))
                                    {
                                        subscribeRequest = new SubscribeEventRequest();
                                        subscribeRequest.EventId = result.Value["EventId"].ToString();
                                        subscribeRequest.EventName = result.Value["EventName"].ToString();
                                        eventList.Add(subscribeRequest);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (eventList.Count > 0)
                {
                    var json = new JavaScriptSerializer().Serialize(eventList);
                    response = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool ChangeSubscriberEvent(string oldInstanceId, string currentInstanceId)
        {
            try
            {
                string getResponseJSON = "";
                bool ChangesMade = false;
                bool isSubscribed = false;
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBEvent.Get();
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;
                        foreach (var result in jObj)
                        {
                            Console.WriteLine(result.Key);
                            //getResponseJSON = (string)result.Value["EventId"];
                            getResponseJSON = result.Value["InstanceId"].ToString();

                            if (!getResponseJSON.ToString().Equals("") && !getResponseJSON.ToString().Equals("null") && !getResponseJSON.ToString().Equals("{}"))
                            {
                                JObject instanceJson = JObject.Parse(getResponseJSON);
                                Console.WriteLine(getResponseJSON);
                                foreach (var instances in instanceJson)
                                {
                                    Console.WriteLine(instances.Key);
                                    if (!instances.Key.Equals("") && (instances.Key == oldInstanceId))
                                    {
                                        isSubscribed = true;
                                        Console.WriteLine("DELETE Request");
                                        FirebaseResponse deleteResponse = firebaseDBEvent
                                            .NodePath(result.Key.ToString() + "/InstanceId/" + oldInstanceId).Delete();

                                        Console.WriteLine(deleteResponse.Success);
                                        if (deleteResponse.Success)
                                        {
                                            FirebaseResponse patchInstanceId = firebaseDBEvent.NodePath(result.Key + "/InstanceId")
                                                                                .Patch("{\"" + currentInstanceId + "\":\"true\"}");
                                            Console.WriteLine(patchInstanceId.Success);
                                            if (patchInstanceId.Success)
                                            {
                                                ChangesMade = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if ((isSubscribed == false) || (isSubscribed && ChangesMade))
                    return true;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
        }

        public AddEventResponse AddEventToVenue(AddEventRequest request)
        {
            string base_uri = URLConstants.AddEventToVenueURL;
            string result = string.Empty;
            string resXML = "";
            XmlSerializer serializer = null;
            AddEventResponse resultEvent = new AddEventResponse();

            try
            {
                base_uri = makeAddEventURI(base_uri, request);
                result = new MakeCall().GetResponseFromWeb(base_uri);

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "response";
                xRoot.IsNullable = true;
                serializer = new XmlSerializer(typeof(AddEventResponse), xRoot);
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    using (TextReader reader = new StringReader(result.ToString()))
                    {
                        object obj = serializer.Deserialize(reader);
                        resultEvent = (AddEventResponse)obj;
                        reader.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return resultEvent;
        }

        public string makeAddEventURI(string base_uri, AddEventRequest request)
        {
            string parameters = string.Empty;
            if (request.title != null && request.title != "")
            {
                parameters = parameters + "title=" + request.title;
            }

            if (request.start_time != null && request.start_time != "")
            {
                //var dateTime = Convert.ToDateTime(request.start_time);
                //request.start_time = dateTime.ToString().Replace(" ", "+");
                parameters = parameters + "&start_time=" + request.start_time;

            }

            if (request.stop_time != null && request.stop_time != "")
            {
                //var dateTime = Convert.ToDateTime(request.stop_time);
                //request.stop_time = dateTime.ToString().Replace(" ", "+");
                parameters = parameters + "&stop_time=" + request.stop_time;
            }

            if (request.all_day)
            { parameters = parameters + "&all_day=" + 1; }
            else
            { parameters = parameters + "&all_day=" + 0; }

            if (request.description != null && request.description != "")
            {
                parameters = parameters + "&description=" + request.description;
            }

            if (request.privacy > 0)
            {
                parameters = parameters + "&privacy=" + request.privacy;
            }

            if (request.tags != null && request.tags != "")
            {
                parameters = parameters + "&tags=" + request.tags;
            }

            if (request.free)
            { parameters = parameters + "&free=" + 1; }
            else
            { parameters = parameters + "&free=" + 0; }

            if (request.price != null && request.price != "")
            {
                parameters = parameters + "&price=" + request.price;
            }

            if (request.venue_id != null && request.venue_id != "")
            {
                parameters = parameters + "&venue_id=" + request.venue_id;
            }

            if (request.parent_id != null && request.parent_id != "")
            {
                parameters = parameters + "&parent_id=" + request.parent_id;
            }

            base_uri = makeAuthenticationURI(parameters, request);

            return base_uri;
        }

        public string makeAuthenticationURI(string Requeststring, AddEventRequest request)
        {
            string URLResult = string.Empty;
            string base_uri = URLConstants.AddEventToVenueURL;
            GetoAuth getoAuth = new GetoAuth();
            var time = Math.Floor(getoAuth.GetTime() / 1000.0);
            string timeStamp = Convert.ToString(time);
            string Nonce = getoAuth.GenerateNonce();

            string parameters = "app_key=" + URLConstants.APIKey
                    + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_token=" + request.oauth_token
                    + "&oauth_version=1.0&" + Requeststring;

            if (parameters.IndexOf(request.start_time) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.start_time);
                parameters = parameters.Replace(request.start_time, URLResult);
            }
            if (parameters.IndexOf(request.description) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.description);
                parameters = parameters.Replace(request.description, URLResult);
            }

            if (parameters.IndexOf(request.title) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.title);
                parameters = parameters.Replace(request.title, URLResult);
            }

            string[] Data = parameters.Split('&');
            Array.Sort(Data, StringComparer.InvariantCultureIgnoreCase);
            parameters = "";
            for (int i = 0; i < Data.Length - 1; i++)
            {
                parameters = parameters + Data[i] + "&";
            }
            parameters = parameters + Data[Data.Length - 1];

            string signature_base_string = getoAuth.GetSignatureBaseString(base_uri, parameters, "get");
            string signature = getoAuth.GetSha1Hash(URLConstants.APISecretKey + "&" + request.oauth_token_secret, signature_base_string);
            Console.WriteLine(signature);

            parameters = "app_key=" + URLConstants.APIKey
                    + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature=" + signature
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_token=" + request.oauth_token
                    + "&oauth_version=1.0&" + Requeststring;

            if (parameters.IndexOf(request.start_time) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.start_time);
                parameters = parameters.Replace(request.start_time, URLResult);
            }
            if (parameters.IndexOf(request.description) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.description);
                parameters = parameters.Replace(request.description, URLResult);
            }
            if (parameters.IndexOf(request.title) > 0)
            {
                URLResult = getoAuth.EscapeDataStingURI(request.title);
                parameters = parameters.Replace(request.title, URLResult);
            }

            Data = parameters.Split('&');
            Array.Sort(Data, StringComparer.InvariantCultureIgnoreCase);
            parameters = "";
            for (int i = 0; i < Data.Length - 1; i++)
            {
                parameters = parameters + Data[i] + "&";
            }
            parameters = parameters + Data[Data.Length - 1];

            URLResult = base_uri + "?" + parameters;
            Console.WriteLine(URLResult);
            return URLResult;
        }
    }
}