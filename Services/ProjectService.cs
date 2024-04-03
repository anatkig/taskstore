using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStore.Data;
using TaskStore.Models;

namespace TaskStore.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();
        }

        public async Task<ProjectModel> GetProjectByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ProjectId cannot be null or empty.");
            }

            return await _projectRepository.GetProjectByIdAsync(id);
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task<ProjectModel> UpdateProjectAsync(string id, ProjectModel project)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ProjectId cannot be null or empty.");
            }

            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            return await _projectRepository.UpdateProjectAsync(id, project);
        }

        public async Task<bool> DeleteProjectAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ProjectId cannot be null or empty.");
            }

            return await _projectRepository.DeleteProjectAsync(id);
        }
    }
}
