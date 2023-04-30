using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UsersWepApiService.DataLayer.DTO;
using UsersWepApiService.DataLayer.Entities;
using UsersWepApiService.DataLayer.Helpers;

namespace UsersWepApiService.DataLayer.Mappers
{
    public class CreatedUserInfoToUserMapperProfile : Profile
    {
        public CreatedUserInfoToUserMapperProfile()
        {
            CreateMap<CreatedUserInfoDTO, User>()
                .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPasswordHelper.GetHashPassword(src.Password)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender != null ? int.Parse(src.Gender) : 2))
                .ForMember(desr=>desr.Birthday, opt=>opt.MapFrom(src => src.Birthday != null ? (DateTime?) DateTime.Parse(src.Birthday) : null))
                .ForMember(dest => dest.Admin, opt => opt.MapFrom(src => bool.Parse(src.Admin)));
        }
    }
}