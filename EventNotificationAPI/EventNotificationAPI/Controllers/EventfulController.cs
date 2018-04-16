using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventNotificationAPI.BusinessLogic;
using EventNotificationAPI.Models;

namespace EventNotificationAPI.Controllers
{
    public class EventfulController : ApiController
    {
        //Test Auth Calls
        [ActionName("GetAuthToken")]
        [HttpGet]
        public HttpResponseMessage TestAuthToken()
        {
            string response = "";
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.GetAuthToken();
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Test Auth Callback
        [ActionName("GetAuthTokenCallback")]
        [HttpGet]
        public HttpResponseMessage GetAuthTokenCallback(string oauth_token, string oauth_verifier, string RequestTokenSecret = "")
        {
            string response = string.Empty;
            try
            {
                CallbackResponseModel callbackResponse = new CallbackResponseModel();
                EventDetails eventDetails = new EventDetails();

                callbackResponse.oauth_token = oauth_token;
                callbackResponse.oauth_verifier = oauth_verifier;
                if (RequestTokenSecret != "")
                    callbackResponse.oauth_request_token_secret = RequestTokenSecret;
                else
                {
                    string message = "Please send Request Token Secret";
                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                response = eventDetails.GetAccessToken(callbackResponse);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get List of Categories
        [ActionName("GetAllCategories")]
        [HttpGet]
        public HttpResponseMessage GetAllCategories()
        {
            string response = "";
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.getCategories();
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get List of Events of a location
        [ActionName("GetAllEventsLocation")]
        [HttpPost]
        public HttpResponseMessage GetAllEventsLocation(EventSearchRequest request)
        {
            string response = "";
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.getAllEvents(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get Event details
        [ActionName("GetEventDetails")]
        [HttpPost]
        public HttpResponseMessage GetEventDetails(GetEventRequest request)
        {
            string response = "";
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.getEventDetails(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get Venue details
        [ActionName("GetVenueDetails")]
        [HttpPost]
        public HttpResponseMessage GetVenueDetails(GetEventRequest request)
        {
            string response = "";
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.getVenueDetails(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get all events in a venue
        [ActionName("GetEventListVenue")]
        [HttpPost]
        public HttpResponseMessage GetEventListVenue(EventSearchRequest request)
        {
            string response = "";
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.getEventByVenue(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get List of Venues of a location
        [ActionName("GetAllVenuesLocation")]
        [HttpPost]
        public HttpResponseMessage GetAllVenuesLocation(VenueSearchRequest request)
        {
            string response = "";
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.getAllVenues(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get List of Venues of a location
        [ActionName("AddEventToVenue")]
        [HttpPost]
        public HttpResponseMessage AddEventToVenue(AddEventRequest request)
        {
            string response = string.Empty;
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.AddEventToVenue(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "false");
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //Get List of Venues of a location
        [ActionName("EditExistingEvent")]
        [HttpPost]
        public HttpResponseMessage EditExistingEvent(VenueSearchRequest request)
        {
            string response = "";
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.getAllVenues(request);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, Ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        //Get all events for an artist
        //[ActionName("GetEventListArtist")]
        //[HttpPost]
        //public HttpResponseMessage GetEventListArtist(string ArtistName)
        //{
        //    var response = 0;
        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}

        //Get all events for an artist
        //[ActionName("GetTest")]
        //[HttpGet]
        //public HttpResponseMessage GetTest()
        //{
        //    var response = 0;
        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}

    }
}
