public interface ITaskService
{
    ProjectTask GetTaskById(string taskId);
    void AddTask(ProjectTask task);
    void EditTask(ProjectTask task);
    void DeleteTask(string taskId);
    IEnumerable<ProjectTask> GetTasksByProjectId(string projectId);
    IEnumerable<ProjectTask> GetAllTasks();
}
