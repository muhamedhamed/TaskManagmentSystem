using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Application;
using TaskManagmentSystem.Application.Dtos;

namespace TaskManagmentSystem.Api;

[ApiController]
[Route("/api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ILogger<TaskController> _logger;

    public TaskController(
        ILogger<TaskController> logger,
        ITaskService taskService)
    {
        _logger = logger ??
                    throw new ArgumentNullException(nameof(logger));
        _taskService = taskService ??
                    throw new ArgumentNullException(nameof(taskService));
    }

    [HttpGet]
    [ActionName("GetAllTasks")]
    public IActionResult GetAllTasks()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all tasks.");

            var tasksDto = _taskService.GetAllTasks();
            if (tasksDto == null || !tasksDto.Any())
            {
                _logger.LogWarning("No tasks found.");
                return NotFound("No tasks found.");
            }
            _logger.LogInformation("Successfully retrieved all tasks.");

            return Ok(tasksDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving tasks: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }


    [HttpGet("{id}")]
    [ActionName("GetTaskById")]
    public IActionResult GetTaskById(string id)
    {
        try
        {
            _logger.LogInformation($"Attempting to retrieve task with ID: {id}");

            if (!Guid.TryParse(id, out _))
            {
                _logger.LogError("Invalid task ID format.");
                return BadRequest("Invalid task ID format.");
            }

            var taskDto = _taskService.GetTaskById(id);

            if (taskDto == null)
            {
                _logger.LogWarning($"Task with ID: {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Successfully retrieved user with ID: {id}");
            return Ok(taskDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving task with ID {id}: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPost]
    [ActionName("AddTask")]
    [Authorize]
    public IActionResult AddTask([FromBody] TaskDto taskDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to create a new task with title: {taskDto.Title}");

            // Validate if the request body is null or empty
            if (taskDto == null)
            {
                _logger.LogError("Invalid task data. Request body cannot be null.");
                return BadRequest("Invalid task data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }


            var result = _taskService.AddTask(taskDto);

            _logger.LogInformation($"Successfully created a new task with ID: {result.TaskId}");
            return CreatedAtAction(nameof(AddTask), result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add task: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPut("{id}")]
    [ActionName("UpdateTask")]
    [Authorize]
    public IActionResult UpdateTask(string id, [FromBody] TaskDto taskDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to update task with ID: {id}");

            if (!Guid.TryParse(id, out _))
            {
                _logger.LogError("Invalid task ID format.");
                return BadRequest("Invalid task ID format.");
            }

            // Check if the request body is null
            if (taskDto == null)
            {
                _logger.LogError("Invalid task data. Request body cannot be null.");
                return BadRequest("Invalid task data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            // Check if the vacancy exists
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                _logger.LogError($"Task with ID {id} not found.");
                return NotFound($"Task with ID {id} not found.");
            }

            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingTask.TaskOwnerId != userIdFromToken)
            {
                _logger.LogError("User does not have permission to update this task info.");
                return Forbid("User does not have permission to update this task info.");
            }

            // Update the user
            _taskService.UpdateTask(taskDto, id);

            _logger.LogInformation($"Successfully updated task with ID: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update task: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpDelete("{id}")]
    [ActionName("DeleteTask")]
    [Authorize]
    public IActionResult DeleteTask(string id)
    {
        try
        {
            _logger.LogInformation($"Attempting to delete task with ID: {id}");

            if (!Guid.TryParse(id, out _))
            {
                _logger.LogError("Invalid task ID format.");
                return BadRequest("Invalid task ID format.");
            }

            // Check if the task exists
            var existingTask = _taskService.GetTaskById(id);
            if (existingTask == null)
            {
                _logger.LogError($"Task with ID {id} not found.");
                return NotFound($"Task with ID {id} not found.");
            }

            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingTask.TaskOwnerId != userIdFromToken)
            {
                _logger.LogError("User does not have permission to delete this task info.");
                return Forbid("User does not have permission to delete this task info.");
            }

            // Delete the task
            _taskService.DeleteTask(id);

            _logger.LogInformation($"Successfully deleted task with ID: {id}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete task: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }


}
