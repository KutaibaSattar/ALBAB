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
            CreateMap<dbAccounts,dbAccountsDto>().ReverseMap();


        }
    }
}