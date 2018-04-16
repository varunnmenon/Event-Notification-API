using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using EventNotificationAPI.DataAccessLayer;
using EventNotificationAPI.RestfulLayer;
using EventNotificationAPI.Models;
using System.Web.Script.Serialization;

namespace EventNotificationAPI.BusinessLogic
{
    public class EventDetails
    {

        public string GetAuthToken()
        {
            string response;
            try
            {
                GetoAuth getoAuth = new GetoAuth();
                response = getoAuth.GetoAuthToken();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string GetAccessToken(CallbackResponseModel callbackResponse)
        {
            string response;
            try
            {
                GetoAuth getoAuth = new GetoAuth();
                response = getoAuth.GetAccessToken(callbackResponse);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string getCategories()
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getEventCategories();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string getEventDetails(GetEventRequest request)
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getEventDetails(request, Globals.eventdetails.FromEvent);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string getAllEvents(EventSearchRequest request)
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getAllEventsLoc(request, Globals.eventdetails.FromEvent);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool SubscribeEvent(SubscribeEventRequest request)
        {
            bool response;
            try
            {
                Event eventObj = new Event();
                response = eventObj.SubscribeEvent(request);
                if (response)
                {
                    MakeFireBaseCalls makeFireBaseCalls = new MakeFireBaseCalls();
                    makeFireBaseCalls.AddtoTopic(Globals.eventdetails.FromEvent, request);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool UnsubscribeEvent(SubscribeEventRequest request)
        {
            bool response;
            try
            {
                Event eventObj = new Event();
                response = eventObj.UnsubscribeEvent(request);
                if (response)
                {
                    MakeFireBaseCalls makeFireBaseCalls = new MakeFireBaseCalls();
                    makeFireBaseCalls.DeleteFromTopic(Globals.eventdetails.FromEvent, request);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string GetSubscribedEvent(LoginRequest request)
        {
            string response = "";
            try
            {
                Event eventObj = new Event();
                response = eventObj.GetSubscribedEvent(request);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string AddEventToVenue(AddEventRequest request)
        {
            string response = string.Empty;
            AddEventResponse result = new AddEventResponse();
            try
            {
                Event eventObj = new Event();
                result = eventObj.AddEventToVenue(request);
                if (result.status != null && result.status != "" && result.status != string.Empty && result.status.ToUpper().Equals("OK"))
                {
                    SendNotification(request,result.id,Globals.eventNotification.AddEvent);
                    var json = new JavaScriptSerializer().Serialize(result);
                    response = json.ToString();

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public void SendNotification(AddEventRequest request, string eventId, Globals.eventNotification eventNotification)
        {
            int TryCount = 0;
            tryAgain:
            try
            {
                MakeFireBaseCalls fireBaseCalls = new MakeFireBaseCalls();
                TryCount++;
                int x = fireBaseCalls.SendPushNotification(request, eventId, eventNotification);
            }
            catch (Exception e)
            {
                if (TryCount < 3)
                    goto tryAgain;
            }
        }
    }
}