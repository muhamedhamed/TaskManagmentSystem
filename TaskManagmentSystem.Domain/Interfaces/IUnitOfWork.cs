using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagmentSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public ITaskRepository TaskRepository { get; }
        void BeginTransaction();
        void SaveChanges();
        void CommitTransaction();
    }
}
