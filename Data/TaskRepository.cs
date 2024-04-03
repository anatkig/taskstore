using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStore.Models;

namespace TaskStore.Data
{
    public class TaskRepository : ITaskRepository
    {
        private readonly CloudTable _table;

        public TaskRepository(CloudTableClient tableClient)
        {
            _table = tableClient.GetTableReference("tasks"); // Table name for tasks
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            var query = new TableQuery<TaskModel>();
            var segment = await _table.ExecuteQuerySegmentedAsync(query, null);
            return segment.Results;
        }

        public async Task<TaskModel> GetTaskByIdAsync(string id)
        {
            var operation = TableOperation.Retrieve<TaskModel>("PartitionKey", id);
            var result = await _table.ExecuteAsync(operation);
            return (TaskModel)result.Result;
        }

        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            task.PartitionKey = Guid.NewGuid().ToString(); // Generate a unique PartitionKey
            task.RowKey = task.TaskId; // Use TaskId as RowKey
            var operation = TableOperation.Insert(task);
            var result = await _table.ExecuteAsync(operation);
            return (TaskModel)result.Result;
        }

        public async Task<TaskModel> UpdateTaskAsync(string id, TaskModel task)
        {
            var existingTask = await GetTaskByIdAsync(id);
            if (existingTask != null)
            {
                existingTask.TaskName = task.TaskName;
                existingTask.TaskDescription = task.TaskDescription;

                var operation = TableOperation.Replace(existingTask);
                var result = await _table.ExecuteAsync(operation);
                return (TaskModel)result.Result;
            }
            return null;
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            var existingTask = await GetTaskByIdAsync(id);
            if (existingTask != null)
            {
                var operation = TableOperation.Delete(existingTask);
                var result = await _table.ExecuteAsync(operation);
                return result.HttpStatusCode == 204; // HTTP 204 indicates success
            }
            return false;
        }
    }
}
