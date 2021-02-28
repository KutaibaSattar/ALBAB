using AutoMapper;
using Entities.DTOs;

namespace ALBaB.Entities.DTOs
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,RegisterDto>().ReverseMap();
           
            CreateMap<AppUser,AppUserDto>().ReverseMap();
           
            CreateMap<AppUser,MemberDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<dbAccounts,dbAccountsDto>().ReverseMap();


        }
    }
}