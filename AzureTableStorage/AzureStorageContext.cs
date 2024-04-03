using Microsoft.Azure.Cosmos.Table;

namespace TaskStore.AzureTableStorage
{
    public class AzureStorageContext
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudTableClient _tableClient;

        public AzureStorageContext(string connectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        }

        public CloudTable GetTableReference(string tableName)
        {
            return _tableClient.GetTableReference(tableName);
        }
    }
}
