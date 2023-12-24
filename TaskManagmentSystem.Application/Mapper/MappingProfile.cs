using AutoMapper;
using TaskManagmentSystem.Application.Dtos;
using TaskManagmentSystem.Domain.Entities;

namespace TaskManagmentSystem.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TaskEntity, TaskDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            // .ForMember(R => R.RoleStr ,D =>D.MapFrom(D => D.Role.ToString()))
            // Add other mappings as needed
        }
    }
}
