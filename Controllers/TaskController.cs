using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;




[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IStorageService _storageService;

    public TasksController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    // GET: api/Tasks
    [HttpGet]
    public IActionResult GetAllTasks()
    {
        IEnumerable<ProjectTask> tasks = _storageService.GetAllTasks();
        return Ok(tasks);
    }
    // GET: api/Tasks/{id}
    [HttpGet("{id}")]
    public IActionResult GetTaskById(string id)
    {
        ProjectTask task = _storageService.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    // GET: api/Tasks/ByProject/{projectId}
    [HttpGet("byproject")]
    public IActionResult GetTasksByProjectId([FromQuery] string projectId)
    {
        if (string.IsNullOrWhiteSpace(projectId))
        {
            return BadRequest("ProjectId is required.");
        }

        IEnumerable<ProjectTask> tasks = _storageService.GetTasksByProjectId(projectId);
        return Ok(tasks);
    }

    // POST: api/Tasks
    [HttpPost]
    public IActionResult AddTask([FromBody] ProjectTask task)
    {
        _storageService.AddTask(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    // PUT: api/Tasks/{id}
    [HttpPut("{id}")]
    public IActionResult EditTask(string id, [FromBody] ProjectTask task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        try
        {
            _storageService.EditTask(task);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }

    // DELETE: api/Tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(string id)
    {
        try
        {
            _storageService.DeleteTask(id);
            return NoContent();
        }
        catch (Exception)
        {
            return NotFound();
        }
    }
}