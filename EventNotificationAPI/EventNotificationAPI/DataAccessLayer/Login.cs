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
    public class Login
    {
        public static FirebaseDB firebaseDBLogin = new FirebaseDB(URLConstants.FirebaseDbURL).Node(URLConstants.FirebaseLoginNode);

        public bool CreateLogin(LoginRequest login)
        {
            try
            {
                string getResponseJSON = "";
                int count = 0;
                FirebaseResponse getResponse = firebaseDBLogin.Get();
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        JObject jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;
                    }
                }
                var dataToInsert = new JavaScriptSerializer().Serialize(login);
                Console.WriteLine("Patch Login Request");
                FirebaseResponse putResponse = firebaseDBLogin.Patch("{\"M" + (count + 1) + "\":" + dataToInsert + "}");
                Console.WriteLine(putResponse.Success);
                if (putResponse.Success)
                    return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
        }

        public bool ModifyLogin(LoginRequest login)
        {
            try
            {
                string getResponseJSON = "";
                string oldInstanceId = "";
                int count = 0;
                FirebaseResponse getResponse = firebaseDBLogin.Get("?orderBy=\"EmailId\"&equalTo=\""+login.EmailId+"\"");
                if (getResponse.Success)
                {
                    if (getResponse.JSONContent != null && !getResponse.JSONContent.ToString().Equals("") && !getResponse.JSONContent.ToString().Equals("null") && !getResponse.JSONContent.ToString().Equals("{}"))
                    {
                        getResponseJSON = getResponse.JSONContent;
                        JObject jObj = JObject.Parse(getResponseJSON);
                        count = jObj.Count;

                        foreach (var results in jObj)
                        {
                            getResponseJSON = results.Key;
                            oldInstanceId = results.Value["InstanceId"].ToString();
                            //Console.WriteLine(results.Key);
                            break;
                        }

                        var dataToInsert = new JavaScriptSerializer().Serialize(login);

                        FirebaseResponse patchResponse = firebaseDBLogin
                        .Node(getResponseJSON)
                        .Patch("{\"InstanceId\":\"" + login.InstanceId + "\"}");
                        Console.WriteLine(patchResponse.Success);

                        if(patchResponse.Success)
                        {
                            //replace Instance Id
                            bool VenueSusbcriptionModify = new Venue().ChangeSubscriberVenue(oldInstanceId,login.InstanceId);
                            bool EventSusbcriptionModify = new Event().ChangeSubscriberEvent(oldInstanceId, login.InstanceId);
                            if (VenueSusbcriptionModify && EventSusbcriptionModify)
                                return true;
                        }
                        //return patchResponse.Success;
                    }
                    else
                    {
                        return CreateLogin(login);
                    }
                }                
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
        }
    }
}