public interface IProjectService
{
    Project GetProjectById(string projectId);
    bool DeleteProject(string projectId);
    void AddProject(Project project);
    void EditProject(Project project);
    IEnumerable<Project> GetAllProjects();
    bool ProjectExists(string projectName);
}
