using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;


namespace ScreenSavingServiceWebRole.Storage
{
    public class UserEntity : TableServiceEntity
    {
        public UserEntity(string userId)
        {
            this.PartitionKey = userId;
            this.RowKey = userId;
        }

        public UserEntity()
        {         
        }

        public string NotificationChannel { get; set; }
        public string NotificationChannelExpiration { get; set; }

    }
}