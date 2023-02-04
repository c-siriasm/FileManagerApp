using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerData.Interfaces;
using System.Configuration;
using Microsoft.Azure.Cosmos;

namespace FileManagerData.Services
{
    public class CosmosRepository<T> : ICosmosRepository<T>
    {
        private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

        private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        private CosmosClient cosmosClient;

        private Database database;

        private Container container;

        private string databaseId = ConfigurationManager.AppSettings["DatabaseId"];
        private string containerId = ConfigurationManager.AppSettings["ContainerId"];

        public CosmosRepository()
        {
            cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
            database = cosmosClient.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
        }

        public async Task<List<T>> GetItems()
        {
            string query = "SELECT * FROM c";
            FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(query);

            List<T> results = new List<T>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (T family in currentResultSet)
                {
                    results.Add(family);
                }
            }

            return results;
        }

        public async Task<bool> CreateItem(T model)
        {
            try
            {
                var myItem = model as FileManagerDTO.Models.File;
                ItemResponse<T> response = await container.CreateItemAsync<T>(model, new PartitionKey(myItem.id));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
