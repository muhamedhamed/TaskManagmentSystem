using TaskManagmentSystem.Domain.Entities;
using TaskManagmentSystem.Domain.Interfaces;

namespace TaskManagmentSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }
        public User GetUserByEmailAndPassword(string email, string password)
        {
            // Find the user by email and password
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}
