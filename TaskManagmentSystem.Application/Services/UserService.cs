using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using TaskManagmentSystem.Application.Interfaces;
using TaskManagmentSystem.Domain.Interfaces.Repositories;
using TaskManagmentSystem.Domain.Interfaces;
using TaskManagmentSystem.Application.Dtos;
using TaskManagmentSystem.Domain.Entities;


namespace TaskManagmentSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository,
        IConfiguration configuration
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public UserDto GetUserById(string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetById(userId);
        return _mapper.Map<UserDto>(userEntity);
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var usersEntities = _unitOfWork.UserRepository.GetAll();
        return _mapper.Map<IEnumerable<UserDto>>(usersEntities);
    }
    
    public UserDto AddUser(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        userEntity.CreatedAt = DateTime.Now;
        userEntity.UpdatedAt = DateTime.Now;
        _unitOfWork.UserRepository.Add(userEntity);
        _unitOfWork.SaveChanges();
        return userDto;
    }

    public UserDto UpdateUser(UserDto userDto, string userId)
    {
        var existingUserEntity = _unitOfWork.UserRepository.GetById(userId);
        _mapper.Map(userDto, existingUserEntity);
        existingUserEntity.UpdatedAt = DateTime.Now;
        _unitOfWork.UserRepository.Update(existingUserEntity);
        _unitOfWork.SaveChanges();

        return _mapper.Map<UserDto>(existingUserEntity);
    }

    public void DeleteUser(string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetById(userId);
        if (userEntity != null)
        {
            _unitOfWork.UserRepository.Remove(userEntity);
            _unitOfWork.SaveChanges();
        }
    }

    public AuthResult Authenticate(string email, string password)
    {
        string userId;
        var user = GetUserByEmailAndPassword(email, password, out userId);

        var token = GenerateJwtToken(userId, email, password);

        return new AuthResult { Success = true, Token = token };
    }

    public UserDto GetUserByEmailAndPassword(string email, string password, out string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetUserByEmailAndPassword(email, password);
        userId = userEntity.UserId;
        return _mapper.Map<UserDto>(userEntity);
    }

    private string GenerateJwtToken(string userId, string email, string password)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("sub", userId),
            new Claim("userId", userId),
            new Claim("email", email),
            new Claim("password", password),
            // You can add additional claims here
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Authentication:ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


