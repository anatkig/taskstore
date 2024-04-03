using Microsoft.Azure.Cosmos.Table;

namespace TaskStore.Models
{
    public class ProjectModel : TableEntity
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        public ProjectModel()
        {
            PartitionKey = "ProjectPartition";
    }
}
