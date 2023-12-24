namespace TaskManagmentSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ITaskRepository TaskRepository { get; }
        public IUserRepository UserRepository { get; }
        void BeginTransaction();
        void SaveChanges();
        void CommitTransaction();
    }
}
