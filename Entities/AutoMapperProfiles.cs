
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
using System.Collections.Generic;
using ALBAB.Entities.Journal;

namespace ALBAB.Entities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser,RegisterDto>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser,AppUserDto>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser,MemberDto>().ForMember(dest => dest.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<dbAccounts,dbAccountsDto>().ReverseMap();

            CreateMap<Brand,BrandDto>();

            CreateMap<Model,ModelDto>();

            CreateMap<Product,ProductDto>()
            .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model.Name));

             CreateMap<Invoice,InvoiceRes>()
             /* .ForMember(dst => dst.Id , opt => opt.Ignore()) */
             .ForMember(dst => dst.invDetails, opt => opt.MapFrom(src => src.InvDetail));

             CreateMap<InvoiceRes,Invoice>()
              .ForMember(dst => dst.Id , opt => opt.Ignore())
               .ForMember(dst => dst.InvDetail, opt => opt.Ignore())
               .AfterMap((phr,ph)=> {

                   // removing deleting items

               var removedItems = new List<InvDetail>();
                foreach (var item in ph.InvDetail)
                   if( phr.invDetails.SingleOrDefault(pd => pd.Id == item.Id) == null)
                    removedItems.Add(item);
                foreach (var item in removedItems)
                    ph.InvDetail.Remove(item);


                   // updated changing
                foreach (var pdd in phr.invDetails)
                   {
                       if (!(pdd.Id > 0)) //New <0
                       {
                             ph.InvDetail.Add( new InvDetail
                            {Price = pdd.Price,
                             Quantity = pdd.Quantity ,
                             ProductId = pdd.ProductId,
                             LastUpdate =DateTime.Now,
                              }
                              );
                       }
                       else
                       {
                           var pd = ph.InvDetail.SingleOrDefault(p => p.Id == pdd.Id);
                           if (pd != null)
                           {
                               if (pd.Price != pdd.Price) pd.Price = pdd.Price;
                               if (pd.Quantity != pdd.Quantity) pd.Quantity = pdd.Quantity;
                               if (pd.ProductId != pdd.ProductId) pd.ProductId = pdd.ProductId;
                           }

                       }
                   }
               });


            CreateMap<InvDetail,InvDetailsRes>();
            CreateMap<InvDetailsRes,InvDetail>();

            CreateMap<JournalEntry,JournalEntryRes>()
                 //.ForMember(dst => dst.Id , opt => opt.Ignore())
                .ForMember(dst => dst.journalAccounts, opt => opt.MapFrom(src => src.journalAccounts));

            CreateMap<JournalEntryRes,JournalEntry>();
                    //.ForMember(dst => dst.Id , opt => opt.Ignore());

            CreateMap<JournalAccount,JournalAccountRes>();
            CreateMap<JournalAccountRes,JournalAccount>();





        }
    }
}