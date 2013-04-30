using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PushNotificationSender;
using Microsoft.WindowsAzure;
using System.Diagnostics;
using ScreenSavingServiceWebRole.Storage;
using ScreenSavingServiceWebRole.Constants;

namespace ScreenSavingServiceWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class PushNotificationService : IPushNotificationService
    {        
        public string GetData(int value)
        {            
            Trace.WriteLine("Shah");
            return string.Format("You entered: {0}", value);
        }

        public string NotifyChannelByUri(string channelUri, string notificationType, string notificationContent)
        {
            WnsAuthorization.OAuthToken oAuthToken = WnsAuthorization.GetOAuthToken(
                CloudConfigurationManager.GetSetting("MetroSecretKey"), CloudConfigurationManager.GetSetting("MetroSid"));
            PushNotifier notifier = new PushNotifier(channelUri, oAuthToken.AccessToken);
            
            if (notifier.SendNotification(notificationType, notificationContent))
            {
                return ReturnType.SUCCESS;
            }
            return ReturnType.FAIL;
        }

        public string NotifyUser(string userId)
        {
            UserEntity entity = UsersTableManager.RetrieveUser(userId);
            if (null == entity || null == entity.NotificationChannel)
            {
                return ReturnType.INVALID_INPUT;
            }

            return NotifyChannelByUri(entity.NotificationChannel, NotificationType.Raw.TYPE, NotificationType.Raw.CONTENT);
        }

        public string RegisterNotificationChannel(string userId, string channelUri, string channelExpiration)
        {
            try
            {
                UsersTableManager.CreateOrUpdateUser(userId, channelUri, channelExpiration);
                return ReturnType.SUCCESS;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString(), LogLevel.ERROR);
                return ReturnType.FAIL;
            }
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
