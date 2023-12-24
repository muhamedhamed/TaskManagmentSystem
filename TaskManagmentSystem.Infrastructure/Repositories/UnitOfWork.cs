using TaskManagmentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManagmentSystem.Domain.Interfaces;

namespace TaskManagmentSystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction _transaction;

    public UnitOfWork(ApplicationDbContext dbContext,
                         ITaskRepository taskRepository,
                         IUserRepository userRepository)
    {
        _dbContext = dbContext;
        TaskRepository = taskRepository;
        UserRepository =userRepository;
    }

    public ITaskRepository TaskRepository { get; }

    public IUserRepository UserRepository { get; }

    public void BeginTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
    public void CommitTransaction()
    {
        _transaction.Commit();
        _transaction.Dispose();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
