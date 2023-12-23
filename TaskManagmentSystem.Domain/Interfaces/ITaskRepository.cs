using TaskManagmentSystem.Domain.Entities;
using TaskManagmentSystem.Domain.Interfaces.Repositories;

namespace TaskManagmentSystem.Domain.Interfaces;

public interface ITaskRepository : IGenericRepository<TaskEntity>
{

}
