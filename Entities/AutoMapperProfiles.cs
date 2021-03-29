
using System.Reflection.Metadata.Ecma335;
using System.IO;
using System.Reflection.PortableExecutable;
using AutoMapper;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;
using ALBAB.Entities.Purchases;
using System.Linq;
using System;

namespace ALBAB.Entities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,RegisterDto>().ForMember(dto => dto.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<AppUser,AppUserDto>().ForMember(dto => dto.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<AppUser,MemberDto>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
           
            CreateMap<dbAccounts,dbAccountsDto>().ReverseMap();

            CreateMap<Brand,BrandDto>();
           
            CreateMap<Model,ModelDto>();

            CreateMap<Product,ProductDto>()
            .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model.Name));

             CreateMap<PurchHDRDto,PurchHDR>()
             .ForMember(dst => dst.Id , opt => opt.Ignore())
             .ForMember(dst => dst.LastUpdate , opt => opt.Ignore())
             .ForMember(dst => dst.purchDTL, opt => opt.MapFrom(src => src.purchDTLDtos))
             /* .AfterMap((Pdto,p) =>{
                 // update new date
                 foreach (var pd in Pdto.purchDTLDtos)
                 {
                     if (!p.purchDTL.Any(p => p.Id == pd.Id))
                        Pdto.purDate = DateTime.Now;

                 }})    */

             .ReverseMap() ;
             
            /*  CreateMap<PurchHDRDto,PurchHDR>()
             .ForMember(dst => dst.Id , opt => opt.Ignore())
             .ForMember(dst => dst.Id , opt => opt.MapFrom(src => src.purchDTLDtos))
             .AfterMap((Pdto,p) =>{
                 // update new date
                 foreach (var pd in Pdto.purchDTLDtos)
                 {
                     if (!p.purchDTL.Any(p => p.Id == pd.Id))
                        Pdto.purDate = DateTime.Now;


                 }   


             })
             .ReverseMap() ; */

            
             CreateMap<PurchDTLDto,PurchDTL>().ReverseMap();
             //.ForMember(dst => dst.LastUpdate , opt => opt.Ignore());
                     /*   .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.Product.Name)); */

 


        }
    }
}