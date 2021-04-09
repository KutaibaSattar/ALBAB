
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
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

             CreateMap<PurchHdr,SavePurchHdrDto>()
             /* .ForMember(dst => dst.Id , opt => opt.Ignore()) */
             .ForMember(dst => dst.purchDtlDtos, opt => opt.MapFrom(src => src.purchDtl));

             CreateMap<SavePurchHdrDto,PurchHdr>()
              .ForMember(dst => dst.Id , opt => opt.Ignore())
               .ForMember(dst => dst.purchDtl, opt => opt.Ignore())
               .AfterMap((phd,ph)=> {

                   // updated changing
                foreach (var pdd in phd.purchDtlDtos)
                   {
                       if (!(pdd.Id > 0)) //New <0
                       {
                             ph.purchDtl.Add( new PurchDtl
                            {Price = pdd.Price,
                             Quantity = pdd.Quantity ,
                             ProductId = pdd.ProductId,
                             LastUpdate =DateTime.Now,
                              }
                              );
                       }
                       else{
                            var pd = ph.purchDtl.SingleOrDefault(p => p.Id == pdd.Id);
                        if (pd != null){
                          if (pd.Price  != pdd.Price) pd.Price = pdd.Price ; 
                          if (pd.Quantity  != pdd.Quantity) pd.Quantity = pdd.Quantity ;
                          if (pd.ProductId != pdd.ProductId) pd.ProductId = pdd.ProductId ;
                       }

                       }       
                  }                     
               });   

               

                         
           
            
             CreateMap<PurchDtl,PurchDtlDto>();
             
             CreateMap<PurchDtlDto,PurchDtl>();
           

 


        }
    }
}