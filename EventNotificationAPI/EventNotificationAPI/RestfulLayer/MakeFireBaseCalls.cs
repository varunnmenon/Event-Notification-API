using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using EventNotificationAPI.Models;
//using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace EventNotificationAPI.RestfulLayer
{
    public class MakeFireBaseCalls
    {
        static string serverKey = URLConstants.FirebaseServerKey;
        static string senderId = URLConstants.FirebaseSenderId;

        public int SendPushNotification()
        {
            try
            {
                //string applicationID = "AIz..........Fep0";
                //string senderId = "30............8";
                //string deviceId = "ch_G60NPga4:APA9............T_LH8up40Ghi-J";
                WebRequest tRequest = WebRequest.Create(URLConstants.FirebaseSendNotifictionURL);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    //to = "ea8Zye6Y5FA:APA91bHziPjBy8TxCaoCraUFkkwoEq7-Gj70dcdM0va8l8w90MXHfpgy8nyXDExpO00AbV48FTQWjKXuztUdFrHDwI_4szWvG3wpdG8OUCTAVRdECLf5EV4vShJWWxzEkCSQwLJk6pYs",
                    to = "edYWRwsijG0:APA91bEp3sd2E_-_-ywck9TRYjiJE8dJiFzcrsNnpUzQS4wE6ib2cpeqGgbSdW7hahEPctJ-OItKzpceeYGeO_nHq0GC14Ci26juTtW7EQKnyZ2b0U2UKwrEjcpN8bloo5gTpvj8XsmB",
                    notification = new
                    {
                        body = "Test1",
                        title = "TestTitle",
                        sound = "Enabled"
                    },

                    android = new
                    {
                        ttl = "500000s",
                        priority = "high"
                    }
                };

                //var serializer = new JavaScriptSerializer();
                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return 0;
            }
            return 1;
        }

        public int SendPushNotification(AddEventRequest request, string eventId, Globals.eventNotification eventNotification)
        {
            string strTitle = string.Empty;
            string strBody = string.Empty;
            string notificationFor = string.Empty;
            try
            {
                switch (eventNotification)
                {
                    case Globals.eventNotification.AddEvent:
                        var Date = Convert.ToDateTime(request.start_time).Date;
                        //DateTime dt = DateTime.ParseExact(Date.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                        string s = Date.ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                        var time = Convert.ToDateTime(request.start_time).ToString("hh:mm tt");
                        strTitle = "New Event Added";
                        //strBody = "New event added at your favourite Venue.Event name : " + request.title + ", on: " + s.ToString() + ", at: " + time.ToString();
                        strBody = "New event added at your favourite Venue.\nEvent name : " + request.title + ", on: " + s + ", at: " + time.ToString();

                        notificationFor = request.venue_id;
                        break;

                    case Globals.eventNotification.EditEvent:
                        var Date1 = Convert.ToDateTime(request.start_time).Date;
                        var time1 = Convert.ToDateTime(request.start_time).ToString("hh:mm tt");
                        strTitle = "New Event Added";
                        strBody = "New event added at your favourite Venue.Event name : " + request.title + ", on: " + Date1.ToString() + ", at: " + time1.ToString();
                        notificationFor = eventId;
                        break;

                    default:
                        break;
                }

                WebRequest tRequest = WebRequest.Create(URLConstants.FirebaseSendNotifictionURL);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    //to = InstanceId,
                    to = "/topics/"+ notificationFor,
                    content_available = true,
                    notification = new
                    {
                        body = strBody,
                        title = strTitle,
                        sound = "Enabled",
                        click_action = URLConstants.FirebaseActivityName
                    },
                    data = new
                    {
                        EventId = eventId
                    },
                    android = new
                    {
                        ttl = "500000s",
                        priority = "high"
                    }
                };
                
                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return 0;
            }
            return 1;
        }

        public string AddtoTopic(Globals.eventdetails eventdetails, SubscribeEventRequest subscribeEvent = null, SubscribeVenueRequest subscribeVenue = null)
        {
            string response = string.Empty;
            string EventId = string.Empty;
            string InstanceId = string.Empty;
            try
            {
                if (eventdetails == Globals.eventdetails.FromEvent)
                {
                    EventId = subscribeEvent.EventId;
                    InstanceId = subscribeEvent.InstanceId;
                }
                else if (eventdetails == Globals.eventdetails.FromVenue)
                {
                    EventId = subscribeVenue.VenueId;
                    InstanceId = subscribeVenue.InstanceId;
                }
                
                WebRequest tRequest = WebRequest.Create(URLConstants.FirebaseSubscribeTopic + "" + InstanceId + "/rel/topics/" + EventId);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.ContentLength = 0;

                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();
                            string str = sResponseFromServer;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public string DeleteFromTopic(Globals.eventdetails eventdetails, SubscribeEventRequest subscribeEvent = null, SubscribeVenueRequest subscribeVenue = null)
        {
            string response = string.Empty;
            string EventId = string.Empty;
            string InstanceId = string.Empty;
            try
            {
                if (eventdetails == Globals.eventdetails.FromEvent)
                {
                    EventId = subscribeEvent.EventId;
                    InstanceId = subscribeEvent.InstanceId;
                }
                else if (eventdetails == Globals.eventdetails.FromVenue)
                {
                    EventId = subscribeVenue.VenueId;
                    InstanceId = subscribeVenue.InstanceId;
                }

                WebRequest tRequest = WebRequest.Create(URLConstants.FirebaseUnSubscribeTopic);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = "/topics/" + EventId,
                    registration_tokens = new string[] {InstanceId}
                };

                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}