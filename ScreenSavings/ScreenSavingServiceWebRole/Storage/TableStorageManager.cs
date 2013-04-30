using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Data.Services.Client;


namespace ScreenSavingServiceWebRole.Storage
{
    public class TableStorageManager<Entity> where Entity : TableServiceEntity 
    {
        private static TableServiceContext CreateTableIfNotExist(string tableName) 
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                                                    CloudConfigurationManager.GetSetting("StorageConnectionString"));            
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            tableClient.CreateTableIfNotExist(tableName);
            return tableClient.GetDataServiceContext();
        }

        public static void UpdateObject(string tableName, Entity entity)
        {
            TableServiceContext serviceContext = CreateTableIfNotExist(tableName);

            //serviceContext.AddObject(tableName, entity);            
            //serviceContext.SaveChangesWithRetries();
            
            //not supported in local
            serviceContext.AttachTo(tableName, entity);
            serviceContext.UpdateObject(entity);
            serviceContext.SaveChangesWithRetries(SaveChangesOptions.ReplaceOnUpdate);
        }

        public static Entity RetrieveObject(string tableName, string partitionKey, string rowKey)
        {        
            TableServiceContext serviceContext = CreateTableIfNotExist(tableName);

            return (from e in serviceContext.CreateQuery<Entity> (tableName)
                         where e.PartitionKey == partitionKey && e.RowKey == rowKey 
                            select e).FirstOrDefault();
        }
    }
}