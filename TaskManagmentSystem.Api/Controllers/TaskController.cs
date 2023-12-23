using Microsoft.AspNetCore.Mvc;
using TaskManagmentSystem.Application;

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
}
