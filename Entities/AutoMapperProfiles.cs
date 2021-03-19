
using System.Reflection.PortableExecutable;
using AutoMapper;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;

namespace ALBAB.Entities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,RegisterDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<AppUser,AppUserDto>().ReverseMap();
           
            CreateMap<AppUser,MemberDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<dbAccounts,dbAccountsDto>().ReverseMap();

            CreateMap<Brand,BrandDto>();
           
            CreateMap<Model,ModelDto>();

            CreateMap<Product,ProductDto>()
            .ForMember(pd => pd.Brand, opt => opt.MapFrom(p => p.Model.Brand.Name))
            .ForMember(pd => pd.Model, opt => opt.MapFrom(p => p.Model.Name));
 


        }
    }
}