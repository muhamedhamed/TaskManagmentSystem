using TaskManagmentSystem.Application.Dtos;

namespace TaskManagmentSystem.Application.Interfaces;

public interface IUserService
{
    UserDto GetUserById(string userId);
    IEnumerable<UserDto> GetAllUsers();
    UserDto AddUser(UserDto userDto);
    UserDto UpdateUser(UserDto userDto,string userId);
    void DeleteUser(string userId);
    UserDto GetUserByEmailAndPassword(string email, string password, out string userId);
    AuthResult Authenticate(string email, string password);
}
