using EventNotificationAPI.Models;
using SimpleBrowser;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;

namespace EventNotificationAPI.RestfulLayer
{
    public class GetoAuth
    {
        public string GetoAuthToken()
        {
            string response = string.Empty;
            //string gotoAddress = string.Empty;
            try
            {
                EventfuloAuthModel GetrequestToken = GetRequestToken();
                if (GetrequestToken.oauth_callback_confirmed.ToUpper() == "TRUE")
                {
                    RequestTokenResponse tokenResponse = new RequestTokenResponse();
                    tokenResponse.URL = URLConstants.AuthorizeTokenBaseURI + "" + GetrequestToken.oauth_token;
                    tokenResponse.RequestTokenSecret = GetrequestToken.oauth_token_secret;
                    var json = new JavaScriptSerializer().Serialize(tokenResponse);
                    response = json.ToString();
                }
                //response = AuthorizeToken(GetrequestToken.oauth_token);
                Console.WriteLine(response);

                #region browser
                //if (response.Contains("DOCTYPE html"))
                //{
                //    //HTMLDocument hTMLDocument = new HTMLDocument();
                //    //hTMLDocument.
                //    //doc.LoadHtml(response);
                //    //doc.GetElementbyId("inp-username").SetAttributeValue("value", "varunMiddleware");
                //    //doc.GetElementbyId("inp-password").SetAttributeValue("value", "Billion@12");
                //    //string gotoAddress = doc.GetElementbyId("signin-form").SelectSingleNode("//input[@type='hidden' and @name='goto']").Attributes["value"].Value;
                //    //var button = doc.GetElementbyId("inp-signin").SetAttributeValue("click", "true");
                //    //HtmlButton htmlButton = (HtmlButton)button;

                //    var browser = new Browser();
                //    browser.SetContent(response);
                //    browser.Url = new Uri("http://eventful.com/signin?goto=/oauth/authorize?oauth_token="+ GetrequestToken.oauth_token);
                //    //browser.CurrentState.Url = new Uri("http://eventful.com/signin");
                //    if (browser.Find("inp-username").Exists)
                //    {
                //        var username = browser.Find("inp-username");
                //        username.Value = ""+Globals.username;
                //    }
                //    if (browser.Find("inp-password").Exists)
                //        browser.Find("inp-password").Value = Globals.password;

                //    if (browser.Find(ElementType.TextField, "name", "goto").Exists)
                //        gotoAddress = browser.Find(ElementType.TextField,"name","goto").Value;

                //    browser.Find("signin-form").SubmitForm();
                //    if (LastRequestFailed(browser)) throw new Exception();

                //    if (browser.ContainsText("Incorrect login or password"))
                //    {
                //        browser.Log("Login failed!", LogMessageType.Error);
                //    }
                //    else
                //    {
                //        browser.Log("* Login Success!!!");
                //    }
                //    //string data = Globals.username + '&' + Globals.password + '&' + gotoAddress;
                //    //response = PostResponseFromWeb(URLConstants.EventfulBaseURI + "signin", data);
                //}
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public string GetAccessToken(CallbackResponseModel callbackResponse)
        {
            string response = string.Empty;
            string requestAccessTokenResp = string.Empty;
            string[] resultData = null;
            AccessTokenResponse accessTokenResponse = new AccessTokenResponse();
            try
            {
                var time = Math.Floor(GetTime() / 1000.0);
                string timeStamp = Convert.ToString(time);
                string Nonce = GenerateNonce();
                //Nonce = "589849682";
                //timeStamp = "1522095584";
                string base_uri = URLConstants.AccessTokenBaseURI;
                string parameters = "oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_token=" + callbackResponse.oauth_token
                    + "&oauth_verifier=" + callbackResponse.oauth_verifier
                    + "&oauth_version=1.0";

                string signature_base_string = GetSignatureBaseString(base_uri, parameters);
                string signature = GetSha1Hash(URLConstants.APISecretKey + "&" + callbackResponse.oauth_request_token_secret, signature_base_string);
                Console.WriteLine(signature);

                base_uri = URLConstants.AccessTokenBaseURI + "?"
                    + "oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature=" + signature
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_token=" + callbackResponse.oauth_token
                    + "&oauth_verifier=" + callbackResponse.oauth_verifier
                    + "&oauth_version=1.0";

                Console.WriteLine(base_uri);

                requestAccessTokenResp = PostResponseFromWeb(base_uri);
                Console.WriteLine(requestAccessTokenResp);

                if (requestAccessTokenResp != string.Empty)
                    resultData = requestAccessTokenResp.Split('&');
                if (resultData != null && resultData.Length > 0)
                {
                    for (int i = 0; i < resultData.Length; i++)
                    {
                        if (resultData[i].Split('=').Length > 0)
                            resultData[i] = resultData[i].Split('=')[1];
                    }

                    accessTokenResponse.oauth_token = resultData[0];
                    accessTokenResponse.oauth_token_secret = resultData[1];

                    var json = new JavaScriptSerializer().Serialize(accessTokenResponse);
                    response = json.ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
        }

        public EventfuloAuthModel GetRequestToken()
        {
            EventfuloAuthModel result = new EventfuloAuthModel();
            string requestTokenResp = string.Empty;
            string[] resultData = null;
            string oauth_token = string.Empty;
            string oauth_token_secret = string.Empty;
            string oauth_callback_confirmed = string.Empty;
            try
            {
                var time = Math.Floor(GetTime() / 1000.0);
                string timeStamp = Convert.ToString(time);
                string Nonce = GenerateNonce();
                //Nonce = "589849682";
                //timeStamp = "1522095584";
                string base_uri = URLConstants.requestTokenBaseURI;
                string parameters = "oauth_callback=" + EscapeDataStingURI(URLConstants.callbackURL)
                    + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_version=1.0";

                string signature_base_string = GetSignatureBaseString(base_uri, parameters);
                string signature = GetSha1Hash(URLConstants.APISecretKey + "&", signature_base_string);
                Console.WriteLine(signature);

                base_uri = URLConstants.requestTokenBaseURI + "?"
                    + "oauth_callback=" + EscapeDataStingURI(URLConstants.callbackURL)
                    + "&oauth_consumer_key=" + URLConstants.APIconsumerKey
                    + "&oauth_nonce=" + Nonce
                    + "&oauth_signature_method=HMAC-SHA1"
                    + "&oauth_timestamp=" + timeStamp
                    + "&oauth_version=1.0"
                    + "&oauth_signature=" + signature;

                Console.WriteLine(base_uri);

                requestTokenResp = PostResponseFromWeb(base_uri);
                Console.WriteLine(result);

                if (requestTokenResp != string.Empty)
                    resultData = requestTokenResp.Split('&');
                if (resultData != null && resultData.Length > 0)
                {
                    for (int i = 0; i < resultData.Length; i++)
                    {
                        if (resultData[i].Split('=').Length > 0)
                            resultData[i] = resultData[i].Split('=')[1];
                    }

                    oauth_callback_confirmed = resultData[2];
                    if (Convert.ToBoolean(oauth_callback_confirmed))
                    {
                        oauth_token = resultData[0];
                        oauth_token_secret = resultData[1];
                    }
                }

                result.oauth_callback_confirmed = oauth_callback_confirmed;
                result.oauth_token = oauth_token;
                result.oauth_token_secret = oauth_token_secret;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return result;
        }

        public string AuthorizeToken(string requestToken)
        {
            string response = string.Empty;
            try
            {
                string baseUri = URLConstants.AuthorizeTokenBaseURI + "" + requestToken;
                response = GetResponseFromWeb(baseUri);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return response;
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

        public string PostResponseFromWeb(string URL, string content = "")
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest Webrequest = (HttpWebRequest)WebRequest.Create(URL); //get count_only
                Webrequest.Method = "POST";
                if (content == "")
                {
                    Webrequest.ContentLength = 0;
                }
                else
                {
                    if (URL.Contains("signin"))
                    {
                        string[] contentArray = content.Split('&');
                        var postData = "username=" + contentArray[0];
                        postData += "&password=" + contentArray[1] + "&goto=" + contentArray[2];
                        var data = Encoding.ASCII.GetBytes(postData);
                        Webrequest.ContentType = "application/x-www-form-urlencoded";
                        Webrequest.ContentLength = data.Length;
                        using (var stream = Webrequest.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                }
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

        public string GetSha1Hash(string key, string message)
        {
            var encoding = new UTF8Encoding();

            byte[] keyBytes = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(message);

            string Sha1Result = string.Empty;

            using (HMACSHA1 SHA1 = new HMACSHA1(keyBytes))
            {
                var Hashed = SHA1.ComputeHash(messageBytes);
                Sha1Result = Convert.ToBase64String(Hashed);
                Sha1Result = EscapeDataStingURI(Sha1Result);
            }

            return Sha1Result;
        }

        public string GetSignatureBaseString(string Signature_Base_URI, string Parameters, string HttpGet = "")
        {
            //1.Convert the HTTP Method to uppercase and set the output string equal to this value.
            string Signature_Base_String = string.Empty;

            if (HttpGet != "")
                Signature_Base_String = "Get";
            else
                Signature_Base_String = "Post";
            Signature_Base_String = Signature_Base_String.ToUpper();

            //2.Append the ‘&’ character to the output string.
            Signature_Base_String = Signature_Base_String + "&";

            //3.Percent encode the URL and append it to the output string.
            string PercentEncodedURL = EscapeDataStingURI(Signature_Base_URI);
            Signature_Base_String = Signature_Base_String + PercentEncodedURL;

            Signature_Base_String = Signature_Base_String + "&";

            PercentEncodedURL = EscapeDataStingURI(Parameters);
            Signature_Base_String = Signature_Base_String + PercentEncodedURL;

            return Signature_Base_String;
        }

        public Int64 GetTime()
        {
            Int64 retval = 0;
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
            retval = (Int64)(t.TotalMilliseconds + 0.5);
            return retval;
        }

        public string GenerateNonce(string extra = "")
        {
            return "" + (Math.Floor(GetRandomNumber(0, 1) * 1e9));
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public string EscapeDataStingURI(string URL)
        {
            string escapedURL = "";
            if (URL != null)
                escapedURL = Uri.EscapeDataString(URL);
            return escapedURL;
        }

        public static bool LastRequestFailed(Browser browser)
        {
            if (browser.LastWebException != null)
            {
                browser.Log("There was an error loading the page: " + browser.LastWebException.Message);
                return true;
            }
            return false;
        }

        public static void OnBrowserMessageLogged(Browser browser, string log)
        {
            Console.WriteLine(log);
        }

        public static void OnBrowserRequestLogged(Browser req, HttpRequestLog log)
        {
            Console.WriteLine(" -> " + log.Method + " request to " + log.Url);
            Console.WriteLine(" <- Response status code: " + log.ResponseCode);
        }
    }
}