using Azure;
using Azure.Data.Tables;
public class ProjectTask : ITableEntity
{


    public ProjectTask()
    {
        PartitionKey = partitionKey;
    }

    const string partitionKey = "TasksPartition";
    public string Id { get; set; } = string.Empty; // Initialized to non-null value
    public string Name { get; set; } = string.Empty; // Initialized to non-null value
    public string Description { get; set; } = string.Empty; // Initialized to non-null value
    public string ProjectId { get; set; } = string.Empty; // Initialized to non-null value
    public string PartitionKey { get; set; }
    public string RowKey { get { return Id; } set { Id = value; } }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}