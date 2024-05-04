using Azure.Data.Tables;
using Azure;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Table;

public class TaskService : ITaskService
{
    private readonly TableClient _taskTableClient;
    private const string partitionKeyTasks = Constants.TasksPartition;

    public TaskService(IOptions<StorageConfig> storageConfig)
    {
        var connectionString = storageConfig.Value.ConnectionString;
        _taskTableClient = new TableClient(connectionString, "tasks");
        _taskTableClient.CreateIfNotExists();
    }

    public ProjectTask GetTaskById(string taskId)
    {
        return _taskTableClient.GetEntity<ProjectTask>(partitionKeyTasks, taskId);
    }

    public void AddTask(ProjectTask task)
    {
        _taskTableClient.AddEntity(task);
    }

    public void EditTask(ProjectTask task)
    {
        _taskTableClient.UpdateEntity(task, ETag.All);
    }

    public void DeleteTask(string taskId)
    {
        _taskTableClient.DeleteEntity(partitionKeyTasks, taskId, ETag.All);
    }

    public IEnumerable<ProjectTask> GetTasksByProjectId(string projectId)
    {
        return _taskTableClient.Query<ProjectTask>().Where(t => t.ProjectId == projectId).ToList();
    }

    public IEnumerable<ProjectTask> GetAllTasks()
    {
        return _taskTableClient.Query<ProjectTask>();
    }
}
