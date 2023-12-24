using TaskManagmentSystem.Domain.Entities;
using TaskManagmentSystem.Domain.Interfaces.Repositories;

namespace TaskManagmentSystem.Domain.Interfaces;

public interface IUserRepository: IGenericRepository<User>
{
     User GetUserByEmailAndPassword(string email, string password);
}
