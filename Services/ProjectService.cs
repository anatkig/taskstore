using Azure.Data.Tables;
using Azure;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Table;

public class ProjectService : IProjectService
{
    private readonly TableClient _projectTableClient;
    private const string partitionKeyProjects = Constants.ProjectsPartition;

    public ProjectService(IOptions<StorageConfig> storageConfig)
    {
        var connectionString = storageConfig.Value.ConnectionString;
        _projectTableClient = new TableClient(connectionString, "projects");
        _projectTableClient.CreateIfNotExists();
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
            return true;
        }
        catch (RequestFailedException e)
        {
            if (e.Status == 404)
            {
                return false;
            }
            throw;
        }
    }

    public bool ProjectExists(string projectName)
    {
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
}
