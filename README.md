# Project Task Storage Service

## Description
This project implements a task storage service where each task belongs to a project. Each task contains at least the following fields: identifier, task name, task description. The system is designed to easily accommodate adding new fields to the task entity. A project entity contains a name, identifier, and code, along with tasks.

The service is deployed on Microsoft Azure using Azure Table Storage to store projects and tasks. A temporary Azure subscription will be used for deployment, which can be created for free.

## Technologies Used
- C# for backend development
- ASP.NET Core for Rest API implementation
- Azure Table Storage for data storage
- Git for version control
- Azure Cloud for deployment

## Getting Started
To clone and run the project locally, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/yourrepository.git
2. cd yourrepository
3. dotnet build
4. dotnet run

## API Endpoints

### Projects
- `GET /api/projects`
  - Retrieve all projects.

- `GET /api/projects/{projectId}`
  - Retrieve a specific project by ID.

- `POST /api/projects`
  - Create a new project.

- `PUT /api/projects/{projectId}`
  - Update an existing project.

- `DELETE /api/projects/{projectId}`
  - Delete a project.

### Tasks
- `GET /api/projects/{projectId}/tasks`
  - Retrieve all tasks for a project.

- `GET /api/tasks/{taskId}`
  - Retrieve a specific task by ID.

- `POST /api/tasks`
  - Create a new task.

- `PUT /api/tasks/{taskId}`
  - Update an existing task.

- `DELETE /api/tasks/{taskId}`
  - Delete a task.
