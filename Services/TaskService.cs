using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStore.Data;
using TaskStore.Models;

namespace TaskStore.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<TaskModel> GetTaskByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("TaskId cannot be null or empty.");
            }

            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<TaskModel> UpdateTaskAsync(string id, TaskModel task)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("TaskId cannot be null or empty.");
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            return await _taskRepository.UpdateTaskAsync(id, task);
        }

        public async Task<bool> DeleteTaskAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("TaskId cannot be null or empty.");
            }

            return await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
