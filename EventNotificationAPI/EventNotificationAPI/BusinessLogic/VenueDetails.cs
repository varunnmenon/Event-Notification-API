using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using EventNotificationAPI.DataAccessLayer;
using EventNotificationAPI.RestfulLayer;
using EventNotificationAPI.Models;

namespace EventNotificationAPI.BusinessLogic
{
    public class VenueDetails
    {

        public string getVenueDetails(GetEventRequest request)
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getEventDetails(request, Globals.eventdetails.FromVenue);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string getEventByVenue(EventSearchRequest request)
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getAllEventsLoc(request, Globals.eventdetails.FromVenue);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string getAllVenues(VenueSearchRequest request)
        {
            string response;
            try
            {
                MakeCall makeCall = new MakeCall();
                response = makeCall.getAllVenuesLoc(request);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool SubscribeVenue(SubscribeVenueRequest request)
        {
            bool response;
            try
            {
                Venue venue = new Venue();
                response = venue.SubscribeVenue(request);
                if (response)
                {
                    MakeFireBaseCalls makeFireBaseCalls = new MakeFireBaseCalls();
                    makeFireBaseCalls.AddtoTopic(Globals.eventdetails.FromVenue, null, request);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public bool UnsubscribeVenue(SubscribeVenueRequest request)
        {
            bool response;
            try
            {
                Venue venue = new Venue();
                response = venue.UnsubscribeVenue(request);
                if (response)
                {
                    MakeFireBaseCalls makeFireBaseCalls = new MakeFireBaseCalls();
                    makeFireBaseCalls.DeleteFromTopic(Globals.eventdetails.FromVenue, null, request);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string GetSubscribedVenue(LoginRequest request)
        {
            string response = "";
            try
            {
                Venue venue = new Venue();
                response = venue.GetSubscribedVenue(request);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

    }
}