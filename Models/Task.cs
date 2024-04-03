using Microsoft.Azure.Cosmos.Table;

namespace TaskStore.Models
{
    public class TaskModel : TableEntity
    {
        public string TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string ProjectId { get; set; }

        public TaskModel()
        {
            PartitionKey = "TaskPartition";
}
