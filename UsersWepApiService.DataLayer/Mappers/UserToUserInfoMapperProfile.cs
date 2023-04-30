using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.Entities;

namespace UsersWepApiService.DataLayer.Mappers
{
    public class UserToUserInfoMapperProfile : Profile
    {
        public UserToUserInfoMapperProfile() 
        {
            CreateMap<User, UserInfoDTO>();
        }
    }
}
