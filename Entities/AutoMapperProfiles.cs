
using System.Reflection.PortableExecutable;
using AutoMapper;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;
using ALBAB.Entities.Purchases;

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
            .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model.Name));

             CreateMap<PurchHDR,PurchHDRDto>();
            
             CreateMap<PurchDTL,PurchDTLDto>()
               .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.Product.Name));

 


        }
    }
}