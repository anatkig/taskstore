using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStore.Models;

namespace TaskStore.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly CloudTable _table;

        public ProjectRepository(CloudTableClient tableClient)
        {
            _table = tableClient.GetTableReference("projects"); // Table name for projects
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            var query = new TableQuery<ProjectModel>();
            var segment = await _table.ExecuteQuerySegmentedAsync(query, null);
            return segment.Results;
        }

        public async Task<ProjectModel> GetProjectByIdAsync(string id)
        {
            var operation = TableOperation.Retrieve<ProjectModel>("PartitionKey", id);
            var result = await _table.ExecuteAsync(operation);
            return (ProjectModel)result.Result;
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
        {
            project.PartitionKey = Guid.NewGuid().ToString(); // Generate a unique PartitionKey
            project.RowKey = project.ProjectId; // Use ProjectId as RowKey
            var operation = TableOperation.Insert(project);
            var result = await _table.ExecuteAsync(operation);
            return (ProjectModel)result.Result;
        }

        public async Task<ProjectModel> UpdateProjectAsync(string id, ProjectModel project)
        {
            var existingProject = await GetProjectByIdAsync(id);
            if (existingProject != null)
            {
                existingProject.ProjectName = project.ProjectName;
                existingProject.ProjectCode = project.ProjectCode;

                var operation = TableOperation.Replace(existingProject);
                var result = await _table.ExecuteAsync(operation);
                return (ProjectModel)result.Result;
            }
            return null;
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            var existingProject = await GetProjectByIdAsync(id);
            if (existingProject != null)
            {
                var operation = TableOperation.Delete(existingProject);
                var result = await _table.ExecuteAsync(operation);
                return result.HttpStatusCode == 204; // HTTP 204 indicates success
            }
            return false;
        }
    }
}
