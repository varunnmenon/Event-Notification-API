using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventNotificationAPI.Models;
using EventNotificationAPI.RestfulLayer;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace EventNotificationAPI.DataAccessLayer
{
    public class Venue
    {
        public static FirebaseDB firebaseDBVenue = new FirebaseDB(URLConstants.FirebaseDbURL).Node(URLConstants.FirebaseVenueNode);

        public bool SubscribeVenue(SubscribeVenueRequest request)
        {
            try
            {
                string getResponseJSON = "";
                bool isExist = false;
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBVenue.Get("?orderBy=\"VenueId\"&equalTo=\"" + request.VenueId + "\"");
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
                            getResponseJSON = (string)result.Value["VenueId"];

                            if (getResponseJSON == request.VenueId)
                            {
                                FirebaseResponse patchInstanceId = firebaseDBVenue
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
                    getResponse = firebaseDBVenue.Get();
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
                    FirebaseResponse patchRequest = firebaseDBVenue.Patch("{\"V" + (count + 1) + "\":{\"VenueId\":\"" + request.VenueId + "\",\"VenueName\":\"" + request.VenueName + "\",\"InstanceId\":{\"" + request.InstanceId + "\":\"true\"}}}");
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

        public bool UnsubscribeVenue(SubscribeVenueRequest request)
        {
            try
            {
                string getResponseJSON = "";
                FirebaseResponse getResponse = firebaseDBVenue.Get("?orderBy=\"VenueId\"&equalTo=\"" + request.VenueId + "\"");
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

                            if ((string)result.Value["VenueId"] == request.VenueId)
                            {
                                Console.WriteLine("DELETE Request");
                                FirebaseResponse deleteResponse = firebaseDBVenue
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

        public string GetSubscribedVenue(LoginRequest request)
        {
            string response = "";
            try
            {
                string getResponseJSON = "";
                IList<SubscribeVenueRequest> venueList = new List<SubscribeVenueRequest>();
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBVenue.Get();
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;
                        foreach (var result in jObj)
                        {
                            SubscribeVenueRequest subscribeRequest = null;
                            //Console.WriteLine(result.Key);
                            if (result.Value["InstanceId"] != null)
                                getResponseJSON = result.Value["InstanceId"].ToString();
                            else
                                getResponseJSON = "";

                            if (!getResponseJSON.ToString().Equals("") && !getResponseJSON.ToString().Equals("null") && !getResponseJSON.ToString().Equals("{}"))
                            {
                                JObject instanceJson = JObject.Parse(getResponseJSON);
                                Console.WriteLine(getResponseJSON);
                                foreach (var instances in instanceJson)
                                {
                                    Console.WriteLine(instances.Key);
                                    if (!instances.Key.Equals("") && (instances.Key == request.InstanceId))
                                    {
                                        subscribeRequest = new SubscribeVenueRequest();
                                        subscribeRequest.VenueId = result.Value["VenueId"].ToString();
                                        subscribeRequest.VenueName = result.Value["VenueName"].ToString();
                                        venueList.Add(subscribeRequest);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (venueList.Count > 0)
                {
                    var json = new JavaScriptSerializer().Serialize(venueList);
                    response = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool ChangeSubscriberVenue(string oldInstanceId, string currentInstanceId)
        {
            try
            {
                string getResponseJSON = "";
                bool ChangesMade = false;
                bool isSubscribed = false;
                int count = 0;
                JObject jObj = null;
                FirebaseResponse getResponse = firebaseDBVenue.Get();
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
                                        FirebaseResponse deleteResponse = firebaseDBVenue
                                            .NodePath(result.Key.ToString() + "/InstanceId/" + oldInstanceId).Delete();

                                        Console.WriteLine(deleteResponse.Success);
                                        if (deleteResponse.Success)
                                        {
                                            FirebaseResponse patchInstanceId = firebaseDBVenue.NodePath(result.Key + "/InstanceId")
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
    }
}