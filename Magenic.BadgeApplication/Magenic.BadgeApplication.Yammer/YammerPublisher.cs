using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Yammer.Helpers;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using Magenic.BadgeApplication.Common;

namespace Magenic.BadgeApplication.Yammer
{
    public class YammerPublisher : IPublisher
    {
        private static string _sessionCookie = string.Empty;
        private static string _yamtrackCookie = string.Empty;
        private static HttpWebResponse _response = null;
        private static HttpWebRequest _request = null;
        private static CookieContainer _cookieContainer = null;

        private string YammerMessageText
        {
            get { return ConfigurationManager.AppSettings["YammerMessage"]; }
        }

        private string Token
        {
            get { return ConfigurationManager.AppSettings["YammerToken"]; }
        }

        private string CurrentUserUrl
        {
            get { return ConfigurationManager.AppSettings["YammerCurrentUserURL"]; }
        }

        private string MessageUrl
        {
            get { return ConfigurationManager.AppSettings["YammerMessageURL"]; }
        }

        private string GetUserUrl(string userEmail)
        {
            return string.Format(ConfigurationManager.AppSettings["YammerGetUserURL"], userEmail);
        }

        public void Publish(EarnedBadgeItemDTO earnedBadge)
        {
            //Get the user that earned the badge
            string userEmail = string.Format("{0}@magenic.com", earnedBadge.EmployeeADName); 
            string userUrl = GetUserUrl(userEmail);

            try
            {
                var response = MakeGetRequest(userUrl, Token);
                YammerUser yammerUser = YammerUser.GetInstanceFromJson(response.Substring(1, response.Length - 2));

                //let's post a message now to this group
                bool broadcastToAll = false;

                string msg = string.Format(YammerMessageText,
                    yammerUser.UserID,
                    earnedBadge.Name,
                    broadcastToAll.ToString(),
					"https://badgeapplication.magenic.com/Leaderboard/show/" + earnedBadge.EmployeeADName,
                    earnedBadge.ImagePath,
                    earnedBadge.Name,
                    earnedBadge.Tagline);

                //try adding the message
                response = MakePostRequest(msg, MessageUrl, Token);
                if (!string.IsNullOrEmpty(response))
                {
                    YammerMessage newMsg = YammerMessage.GetInstanceFromJson(response);
                }
            }
            catch (WebException ex)
            {
                var httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    Logger.Warn<YammerPublisher>(string.Format("Problem getting Yammer information for URL: {0}.  Most likely cause is the user was not setup in Yammer, continuing to process.", userUrl));                    
                }
                else
                {
                    throw;
                }
            }
        }

        private string MakeGetRequest(string url, string token)
        {
            return MakeGetRequest(url, token, false);
        }

        private string MakeGetRequest(string Url, string authHeader, bool AddCookies)
        {
            string results = string.Empty;

            _cookieContainer = new CookieContainer(2);
            _request = WebRequest.CreateHttp(Url);
            _request.Method = "GET";

            if (AddCookies)
            {
                SetCookies();
                _request.CookieContainer = _cookieContainer;
            }                

            if (!string.IsNullOrEmpty(authHeader))
                _request.Headers.Add("Authorization", "Bearer " + authHeader);

            _response = (HttpWebResponse)_request.GetResponse();

            Stream dataStream = _response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            results = reader.ReadToEnd();

            reader.Close();

            return results;
        }

        private string MakePostRequest(string postBody, string url, string authHeader = null, string contentType = null)
        {
            string results = string.Empty;
            SetCookies();

            _request = WebRequest.CreateHttp(url);
            _request.Method = "POST";
            _request.CookieContainer = _cookieContainer;
                                
            if (!string.IsNullOrEmpty(authHeader))
                _request.Headers.Add("Authorization", "Bearer " + authHeader);

            byte[] postByte = Encoding.UTF8.GetBytes(postBody);

            if (string.IsNullOrEmpty(contentType))
                _request.ContentType = "application/x-www-form-urlencoded";
            else
                _request.ContentType = contentType;

            _request.ContentLength = postByte.Length;
            Stream postStream = _request.GetRequestStream();
            postStream.Write(postByte, 0, postByte.Length);
            postStream.Close();

            _response = (HttpWebResponse)_request.GetResponse();
            postStream = _response.GetResponseStream();
            StreamReader postReader = new StreamReader(postStream);

            results = postReader.ReadToEnd();

            postReader.Close();

            return results;
        }

        private static void SetCookies()
        {
            const string YAMTRAK_COOKIE = "yamtrak_id";
            const string SESSION_COOKIE = "_workfeed_session_id";
                             
            string cookies = _response.Headers["Set-Cookie"];

            if (string.IsNullOrEmpty(cookies))
            {
                _cookieContainer = new CookieContainer();
            }
            else
            {
                int cStart = cookies.IndexOf("=");
                int cEnd = cookies.IndexOf("HttpOnly,");

                //sometimes the cookie ends with "HttpOnly," and sometimes it ends with "secure"
                if (
                    (cookies.Substring(cStart + 1, cEnd + 8 - cStart - 1).IndexOf(YAMTRAK_COOKIE) > -1) ||
                        (cookies.Substring(cStart + 1, cEnd + 8 - cStart - 1).IndexOf(SESSION_COOKIE) > -1)
                    )
                {
                    //change the end to look for secure
                    cEnd = cookies.IndexOf("secure,");
                }

                string tempCook1 = cookies.Substring(cStart + 1, cEnd + 8 - cStart - 1);
                tempCook1 = tempCook1.Remove(tempCook1.IndexOf(";"));

                cStart = cookies.IndexOf("=", cEnd);
                string tempCook2 = cookies.Substring(cStart + 1);
                tempCook2 = tempCook2.Remove(tempCook2.IndexOf(";"));

                if (cookies.StartsWith("yamtrak"))
                {
                    _yamtrackCookie = tempCook1;
                    _sessionCookie = tempCook2;
                }
                else
                {
                    _sessionCookie = tempCook1;
                    _yamtrackCookie = tempCook2;
                }

                _cookieContainer = new CookieContainer();
                _cookieContainer.Add(new Cookie(YAMTRAK_COOKIE, _yamtrackCookie, "/", "www.yammer.com"));
                _cookieContainer.Add(new Cookie(SESSION_COOKIE, _sessionCookie, "/", "www.yammer.com"));
            }
        }
    }
}
