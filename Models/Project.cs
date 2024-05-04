using Azure;
using Azure.Data.Tables;
using System.Text.Json;

public class Project : ITableEntity
{
    public Project()
    {
        PartitionKey = partitionKey;
    }
    const string partitionKey = Constants.ProjectsPartition;
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty; 
    public string Code { get; set; } = string.Empty; 
    public string Complexity { get; set; } = string.Empty; 
    public string PartitionKey { get; set; }
    public string RowKey { get { return Id; } set { Id = value; } }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}