using AutoMapper;
using Microsoft.Extensions.Configuration;
using TaskManagmentSystem.Domain.Entities;
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

    public IEnumerable<TaskDto> GetAllTasks()
    {
        var tasksEntities = _unitOfWork.TaskRepository.GetAll();
        return _mapper.Map<IEnumerable<TaskDto>>(tasksEntities);
    }

    public TaskDto GetTaskById(string taskId)
    {
        var taskEntity = _unitOfWork.TaskRepository.GetById(taskId);
        return _mapper.Map<TaskDto>(taskEntity);
    }

    public TaskDto AddTask(TaskDto taskDto)
    {
        var taskEntity = _mapper.Map<TaskEntity>(taskDto);
        taskEntity.CreatedAt = DateTime.Now;
        taskEntity.UpdatedAt = DateTime.Now;
        _unitOfWork.TaskRepository.Add(taskEntity);
        _unitOfWork.SaveChanges();
        return taskDto;
    }

    public TaskDto UpdateTask(TaskDto taskDto, string taskId)
    {
        // Ad more validation
        var existingTaskEntity = _unitOfWork.TaskRepository.GetById(taskId);
        _mapper.Map(taskDto, existingTaskEntity);
        existingTaskEntity.UpdatedAt = DateTime.Now;
        _unitOfWork.TaskRepository.Update(existingTaskEntity);
        _unitOfWork.SaveChanges();

        return _mapper.Map<TaskDto>(existingTaskEntity);
    }

    public void DeleteTask(string taskId)
    {
        var taskEntity = _unitOfWork.TaskRepository.GetById(taskId);
        if (taskEntity != null)
        {
            _unitOfWork.TaskRepository.Remove(taskEntity);
            _unitOfWork.SaveChanges();
        }
    }
}
