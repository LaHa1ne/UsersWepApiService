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
    public class UserToUserMainInfoMapperProfile : Profile
    {
        public UserToUserMainInfoMapperProfile() 
        {
            CreateMap<User, UserMainPersonalInfoDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.RevokedOn == null));

        }
    }

}
