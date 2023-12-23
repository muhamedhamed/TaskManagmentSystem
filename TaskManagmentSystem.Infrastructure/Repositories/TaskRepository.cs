using TaskManagmentSystem.Domain.Interfaces;
using TaskManagmentSystem.Domain.Entities;
using TaskManagmentSystem.Infrastructure.Repositories;
using TaskManagmentSystem.Domain.Interfaces.Repositories;

namespace TaskManagmentSystem.Infrastructure;

public class TaskRepository : GenericRepository<TaskEntity>, ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
