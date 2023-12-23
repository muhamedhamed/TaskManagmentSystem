using AutoMapper;
using Microsoft.Extensions.Configuration;
using TaskManagmentSystem.Domain.Interfaces;
using TaskManagmentSystem.Domain.Interfaces.Repositories;

namespace TaskManagmentSystem.Application.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    // private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    // private readonly IConfiguration _configuration;

    public TaskService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITaskRepository taskRepository,
        IConfiguration configuration
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        // _taskRepository = taskRepository;
        // _configuration = configuration;
    }

    public TaskDto AddTask(TaskDto userDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteTask(string userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TaskDto> GetAllTasks()
    {
          var tasksEntities = _unitOfWork.TaskRepository.GetAll();
        return _mapper.Map<IEnumerable<TaskDto>>(tasksEntities);
    }

    public TaskDto GetTaskById(string userId)
    {
        throw new NotImplementedException();
    }

    public TaskDto UpdateTask(TaskDto userDto, string userId)
    {
        throw new NotImplementedException();
    }
}
