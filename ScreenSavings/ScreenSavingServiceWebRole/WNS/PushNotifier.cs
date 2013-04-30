using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using System.Diagnostics;
using ScreenSavingServiceWebRole.Constants;

namespace PushNotificationSender
{
    public class PushNotifier
    {
        private string _uri;
        private string _accessToken;
   
        public PushNotifier(string uri, string accessToken)
        {
            _uri = uri;
            _accessToken = accessToken;
        }

        public bool SendNotification(String pushType, string notificationContent)
        {
            bool result = false;            

            try
            {
                string rawType = "wns/" + pushType;                                
                var subscriptionUri = new Uri(_uri);
                var request = (HttpWebRequest)WebRequest.Create(subscriptionUri);
                request.Method = "POST";
                request.ContentType = "text/xml";
                request.Headers = new WebHeaderCollection();
                request.Headers.Add("X-WNS-Type", rawType);
                request.Headers.Add("Authorization", "Bearer " + _accessToken);
                byte[] notificationMessage = Encoding.Default.GetBytes(notificationContent);
                request.ContentLength = notificationContent.Length;
                
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(notificationMessage, 0, notificationMessage.Length);
                }
 
                var response = (HttpWebResponse)request.GetResponse();
 
                result = (response.StatusCode == HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                result = false;
                Trace.WriteLine(e.ToString(), LogLevel.ERROR);               
            }
 
            return result;
        }
    }
}
