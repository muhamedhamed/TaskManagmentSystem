using TaskManagmentSystem.Application.Dtos;

namespace TaskManagmentSystem.Application;

public interface ITaskService
{
    TaskDto GetTaskById(string userId);
    IEnumerable<TaskDto> GetAllTasks();
    TaskDto AddTask(TaskDto userDto);
    TaskDto UpdateTask(TaskDto userDto, string userId);
    void DeleteTask(string userId);
}
