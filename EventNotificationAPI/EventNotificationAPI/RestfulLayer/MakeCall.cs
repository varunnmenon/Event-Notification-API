using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using EventNotificationAPI.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Xml;
using System.Security.Cryptography;
using System.Text;

namespace EventNotificationAPI.RestfulLayer
{
    public class MakeCall
    {
        public string getAllEventsLoc(EventSearchRequest request, Globals.eventdetails eventdetails)
        {
            string base_uri;
            
            switch (eventdetails)
            {
                case Globals.eventdetails.FromEvent:
                    base_uri = URLConstants.SearchEventsByLocation + "where=" + request.latitude + "," + request.longitude + "&within=25";
                    break;

                case Globals.eventdetails.FromVenue:
                    base_uri = URLConstants.SearchEventsByLocation + "location=" + request.location;
                    break;

                default:
                    throw new Exception();
            }

            if (!string.IsNullOrEmpty(request.category))
            {
                base_uri = base_uri + "&category="+ Convert.ToString(request.category);
            }

            string result = string.Empty;
            string resXML = "";
            XmlSerializer serializer = null;
            SearchEventResp resultEvent = null;

            try
            {
                result = GetResponseFromWeb(base_uri + "&count_only=true");
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    var resultXML = str.Elements("total_items").Where(x => !(x.Value.Equals(""))).ToList();
                    if (resultXML != null && resultXML.Count > 0 && resultXML[0] != null)
                        resXML = resultXML[0].Value.ToString();
                }

                //result = GetResponseFromWeb(base_uri + "&page_size=" + resXML);
                result = GetResponseFromWeb(base_uri + "&page_size=20");
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "search";
                xRoot.IsNullable = true;
                serializer = new XmlSerializer(typeof(SearchEventResp), xRoot);
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    using (TextReader reader = new StringReader(result.ToString()))
                    {
                        object obj = serializer.Deserialize(reader);
                        resultEvent = (SearchEventResp)obj;
                        reader.Close();
                    }
                }

                if (resultEvent != null)
                {
                    var json = new JavaScriptSerializer().Serialize(resultEvent);
                    result = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public string getEventDetails(GetEventRequest request, Globals.eventdetails eventdetails)
        {

            string base_uri;

            switch (eventdetails)
            {
                case Globals.eventdetails.FromEvent:
                    base_uri = URLConstants.EventDetailsURL + "id=" + request.id;
                    break;

                case Globals.eventdetails.FromVenue:
                    base_uri = URLConstants.VenueDetailsURL + "id=" + request.id;
                    break;

                default:
                    throw new Exception();
            }

            string result = string.Empty;
            XmlDocument xml;

            try
            {
                result = GetResponseFromWeb(base_uri);
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    if (str != null && str.ToString() != "")
                    {
                        xml = new XmlDocument();
                        xml.LoadXml(str.ToString());

                        if (xml != null)
                        {
                            var jsonText = JsonConvert.SerializeXmlNode(xml);
                            result = jsonText.ToString();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public string getAllVenuesLoc(VenueSearchRequest request)
        {
            string base_uri = URLConstants.SearchVenuesByLocation + "location=" + request.latitude + "," + request.longitude + "&within=25";

            string result = string.Empty;
            string resXML = "";
            XmlSerializer serializer = null;
            VenueSearchResponse resultEvent = null;

            try
            {
                result = GetResponseFromWeb(base_uri + "&count_only=true");
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    var resultXML = str.Elements("total_items").Where(x => !(x.Value.Equals(""))).ToList();
                    if (resultXML != null && resultXML.Count > 0 && resultXML[0] != null)
                        resXML = resultXML[0].Value.ToString();
                }

                //result = GetResponseFromWeb(base_uri + "&page_size=" + resXML);
                result = GetResponseFromWeb(base_uri + "&page_size=20");

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "search";
                xRoot.IsNullable = true;
                serializer = new XmlSerializer(typeof(VenueSearchResponse), xRoot);
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    using (TextReader reader = new StringReader(result.ToString()))
                    {
                        object obj = serializer.Deserialize(reader);
                        resultEvent = (VenueSearchResponse)obj;
                        reader.Close();
                    }
                }

                if (resultEvent != null)
                {
                    var json = new JavaScriptSerializer().Serialize(resultEvent);
                    result = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public string getEventCategories()
        {
            string base_uri = URLConstants.GetCategoriesURL;

            string result = string.Empty;
            //string resXML = "";
            XmlSerializer serializer = null;
            CategoryResponse resultEvent = null;

            try
            {
                result = GetResponseFromWeb(base_uri);

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "categories";
                xRoot.IsNullable = true;
                serializer = new XmlSerializer(typeof(CategoryResponse), xRoot);
                if (result != null && result.Length > 0)
                {
                    var str = XElement.Parse(result);
                    using (TextReader reader = new StringReader(result.ToString()))
                    {
                        object obj = serializer.Deserialize(reader);
                        resultEvent = (CategoryResponse)obj;
                        reader.Close();
                    }
                }

                if (resultEvent != null)
                {
                    var json = new JavaScriptSerializer().Serialize(resultEvent);
                    result = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public string GetResponseFromWeb(string URL)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest Webrequest = (HttpWebRequest)WebRequest.Create(URL); //get count_only
                using (HttpWebResponse response = (HttpWebResponse)Webrequest.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        #region CommentedCode

        //public string TestAuthToken()
        //{
        //    string result = string.Empty;
        //    string[] resultData = null;
        //    string oauth_token = string.Empty;
        //    string oauth_token_secret = string.Empty;
        //    string oauth_callback_confirmed = string.Empty;
        //    try
        //    {
        //        var time = Math.Floor(GetTime() / 1000.0);
        //        string timeStamp = Convert.ToString(time);
        //        string Nonce = GenerateNonce();
        //        //Nonce = "589849682";
        //        //timeStamp = "1522095584";
        //        string base_uri = "http://eventful.com/oauth/request_token";
        //        string parameters = "oauth_callback=" + EscapeDataStingURI(URLConstants.callbackURL)
        //            + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
        //            + "&oauth_nonce=" + Nonce
        //            + "&oauth_signature_method=HMAC-SHA1"
        //            + "&oauth_timestamp=" + timeStamp
        //            + "&oauth_version=1.0";

        //        string signature_base_string = GetSignatureBaseString(base_uri, parameters, timeStamp, Nonce);
        //        string signature = GetSha1Hash(URLConstants.APISecretKey + "&", signature_base_string);
        //        Console.WriteLine(signature);

        //        base_uri = "http://eventful.com/oauth/request_token?"
        //            + "oauth_callback=" + EscapeDataStingURI(URLConstants.callbackURL)
        //            + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
        //            + "&oauth_nonce=" + Nonce
        //            + "&oauth_signature_method=HMAC-SHA1"
        //            + "&oauth_timestamp=" + timeStamp
        //            + "&oauth_version=1.0"
        //            + "&oauth_signature=" + signature;

        //        Console.WriteLine(base_uri);

        //        result = PostResponseFromWeb(base_uri);
        //        Console.WriteLine(result);

        //        if (result != string.Empty)
        //            resultData = result.Split('&');
        //        if (resultData != null && resultData.Length > 0)
        //        {
        //            for (int i = 0; i < resultData.Length; i++)
        //            {
        //                if (resultData[i].Split('=').Length > 0)
        //                    resultData[i] = resultData[i].Split('=')[1];
        //            }

        //            oauth_callback_confirmed = resultData[2];
        //            if (Convert.ToBoolean(oauth_callback_confirmed))
        //            {
        //                oauth_token = resultData[0];
        //                oauth_token_secret = resultData[1];
        //            }
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    return result;
        //}

        //public string PostResponseFromWeb(string URL)
        //{
        //    string result = string.Empty;
        //    try
        //    {
        //        HttpWebRequest Webrequest = (HttpWebRequest)WebRequest.Create(URL); //get count_only
        //        Webrequest.Method = "POST";
        //        Webrequest.ContentLength = 0;
        //        using (HttpWebResponse response = (HttpWebResponse)Webrequest.GetResponse())
        //        using (Stream stream = response.GetResponseStream())
        //        using (StreamReader reader = new StreamReader(stream))
        //        {
        //            result = reader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    return result;
        //}

        //public string GetSha1Hash(string key, string message)
        //{
        //    var encoding = new UTF8Encoding();

        //    byte[] keyBytes = encoding.GetBytes(key);
        //    byte[] messageBytes = encoding.GetBytes(message);

        //    string Sha1Result = string.Empty;

        //    using (HMACSHA1 SHA1 = new HMACSHA1(keyBytes))
        //    {
        //        var Hashed = SHA1.ComputeHash(messageBytes);
        //        Sha1Result = Convert.ToBase64String(Hashed);
        //        Sha1Result = EscapeDataStingURI(Sha1Result);
        //    }

        //    return Sha1Result;
        //}

        //public string GetSignatureBaseString(string Signature_Base_URI, string Parameters, string TimeStamp, string Nonce)
        //{
        //    //1.Convert the HTTP Method to uppercase and set the output string equal to this value.
        //    string Signature_Base_String = "Post";
        //    Signature_Base_String = Signature_Base_String.ToUpper();

        //    //2.Append the ‘&’ character to the output string.
        //    Signature_Base_String = Signature_Base_String + "&";

        //    //3.Percent encode the URL and append it to the output string.
        //    string PercentEncodedURL = EscapeDataStingURI(Signature_Base_URI);
        //    Signature_Base_String = Signature_Base_String + PercentEncodedURL;

        //    Signature_Base_String = Signature_Base_String + "&";

        //    PercentEncodedURL = EscapeDataStingURI(Parameters);
        //    Signature_Base_String = Signature_Base_String + PercentEncodedURL;

        //    return Signature_Base_String;
        //}

        //private Int64 GetTime()
        //{
        //    Int64 retval = 0;
        //    var st = new DateTime(1970, 1, 1);
        //    TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
        //    retval = (Int64)(t.TotalMilliseconds + 0.5);
        //    return retval;
        //}

        //public string GenerateNonce(string extra = "")
        //{
        //    return "" + (Math.Floor(GetRandomNumber(0, 1) * 1e9));
        //}

        //public double GetRandomNumber(double minimum, double maximum)
        //{
        //    Random random = new Random();
        //    return random.NextDouble() * (maximum - minimum) + minimum;
        //}

        //public string EscapeDataStingURI(string URL)
        //{
        //    string escapedURL = "";
        //    if (URL != null)
        //        escapedURL = Uri.EscapeDataString(URL);
        //    return escapedURL;
        //}
        #endregion
    }
}