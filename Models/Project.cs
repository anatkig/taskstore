using Azure;
using Azure.Data.Tables;
using System.Text.Json;

public class Project : ITableEntity
{
    public Project()
    {
        PartitionKey = partitionKey;
    }
    const string partitionKey = "ProjectsPartition";
    public string Id { get; set; } = string.Empty;
    // Initialized to non-null value
    public string Name { get; set; } = string.Empty; // Initialized to non-null value
    public string Description { get; set; } = string.Empty; // Initialized to non-null value
    public string Code { get; set; } = string.Empty; // Initialized to non-null value

    public string Complexity { get; set; } = string.Empty; // Initialized to non-null value

    public string PartitionKey { get; set; }
    public string RowKey { get { return Id; } set { Id = value; } }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}