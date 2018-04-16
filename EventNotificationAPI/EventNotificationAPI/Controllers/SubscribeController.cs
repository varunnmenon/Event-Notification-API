using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventNotificationAPI.Models;
using EventNotificationAPI.BusinessLogic;

namespace EventNotificationAPI.Controllers
{
    public class SubscribeController : ApiController
    {
        //Subscribe to an event
        [ActionName("SubscribeEvent")]
        [HttpPost]
        public HttpResponseMessage SubscribeEvent(SubscribeEventRequest request)
        {
            bool response = false;
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.SubscribeEvent(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //subscribe to a venue
        [ActionName("SubscribeVenue")]
        [HttpPost]
        public HttpResponseMessage SubscribeVenue(SubscribeVenueRequest request)
        {
            bool response = false;
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.SubscribeVenue(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //Unsubscribe to an event
        [ActionName("UnsubscribeEvent")]
        [HttpPost]
        public HttpResponseMessage UnsubscribeEvent(SubscribeEventRequest request)
        {
            bool response = false;
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.UnsubscribeEvent(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //Unsubscribe to a venue
        [ActionName("UnsubscribeVenue")]
        [HttpPost]
        public HttpResponseMessage UnsubscribeVenue(SubscribeVenueRequest request)
        {
            bool response = false;
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.UnsubscribeVenue(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //get All Subscribed Venue
        [ActionName("GetSubscribedVenue")]
        [HttpPost]
        public HttpResponseMessage GetSubscribedVenue(LoginRequest request)
        {
            string response = "";
            try
            {
                VenueDetails venueDetails = new VenueDetails();
                response = venueDetails.GetSubscribedVenue(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        //get All Subscribed Events
        [ActionName("GetSubscribedEvent")]
        [HttpPost]
        public HttpResponseMessage GetSubscribedEvent(LoginRequest request)
        {
            string response = "";
            try
            {
                EventDetails eventDetails = new EventDetails();
                response = eventDetails.GetSubscribedEvent(request);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
