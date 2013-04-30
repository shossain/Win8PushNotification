using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScreenSavingServiceWebRole.Storage
{
    public class UsersTableManager
    {
        private static string TABLE_NAME = "users";

        public static void CreateOrUpdateUser (string userId, string notificationChannel, string notificationChannelExpiration)
        {
            UserEntity entity = new UserEntity (userId);

            entity.NotificationChannel = notificationChannel;
            entity.NotificationChannelExpiration = notificationChannelExpiration;

            TableStorageManager<UserEntity>.UpdateObject(TABLE_NAME, entity); 
        }

        public static UserEntity RetrieveUser(string userId)
        {
            return TableStorageManager<UserEntity>.RetrieveObject(TABLE_NAME, userId, userId);
        }
    }
}