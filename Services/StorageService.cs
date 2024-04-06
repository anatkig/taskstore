using Azure.Data.Tables;
using Azure;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Table;


public class StorageConfig
{
    public string? ConnectionString { get; set; }
}
public interface IStorageService
{
    Project GetProjectById(string projectId);
    bool DeleteProject(string projectId);
    void AddProject(Project project);
    void EditProject(Project project);
    IEnumerable<Project> GetAllProjects();
    IEnumerable<ProjectTask> GetAllTasks();
    ProjectTask GetTaskById(string taskId);
    void AddTask(ProjectTask task);
    void EditTask(ProjectTask task);
    void DeleteTask(string taskId);
    public bool ProjectExists(string projectName);

    IEnumerable<ProjectTask> GetTasksByProjectId(string projectId);
}

public class StorageService : IStorageService
{
    private readonly TableClient _projectTableClient;
    private readonly TableClient _taskTableClient;

    const string partitionKeyTasks = "TasksPartition";
    const string partitionKeyProjects = "ProjectsPartition";

    public StorageService(IOptions<StorageConfig> storageConfig)
    {
        var connectionString = storageConfig.Value.ConnectionString;
        _projectTableClient = new TableClient(connectionString, "projects");
        _taskTableClient = new TableClient(connectionString, "tasks");

        _projectTableClient.CreateIfNotExists();
        _taskTableClient.CreateIfNotExists();
    }

    public Project GetProjectById(string projectId)
    {
        try
        {
            var response = _projectTableClient.GetEntity<Project>(partitionKeyProjects, projectId);
            return response.Value;
        }
        catch (RequestFailedException e) when (e.Status == 404)
        {
            throw new Exception($"Project with ID {projectId} not found.", e);
        }
    }
    public bool DeleteProject(string projectId)
    {
        try
        {
            _projectTableClient.DeleteEntity(partitionKeyProjects, projectId, ETag.All);
            return true; // Return true if the deletion is successful
        }
        catch (RequestFailedException e)
        {
            if (e.Status == 404)  // Not found
            {
                return false;
            }
            throw;
        }
    }

    public bool ProjectExists(string projectName)
    {
        // A more efficient design might involve querying based on PartitionKey and RowKey, or using Azure Cosmos DB for advanced querying capabilities.
        // But since we have a small project and we are not using Cosmos DB, I think it is fine.s
        var projects = _projectTableClient.Query<Project>(filter: $"PartitionKey eq '{partitionKeyProjects}'");
        return projects.Any(p => p.Name == projectName);
    }

    public void AddProject(Project project)
    {
        _projectTableClient.AddEntity(project);
    }

    public void EditProject(Project project)
    {
        _projectTableClient.UpdateEntity(project, ETag.All);
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return _projectTableClient.Query<Project>();
    }

    // Tasks CRUD:

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