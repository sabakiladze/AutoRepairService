using AutoMapper;
using AutoRepairService.Application.Dtos.UserDto;
using AutoRepairService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRepairService.Application.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequestDto, User>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();
        }
    }
}
