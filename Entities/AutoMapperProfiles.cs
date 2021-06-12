
using System.Security.Cryptography.X509Certificates;
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
            CreateMap<AppUser, RegisterDto>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser, AppUserDto>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser, MemberDto>()
                .ForMember(dst => dst.KeyId, opt => opt.MapFrom(src => src.UserName));


            CreateMap<dbAccounts, dbAccountsFlattenRes>();




            CreateMap<dbAccounts, dbAccountsDto>().ReverseMap();

            CreateMap<Brand, BrandDto>();

            CreateMap<Model, ModelDto>();

            CreateMap<Product, ProductDto>()
            .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Invoice, InvoiceRes>()
            /* .ForMember(dst => dst.Id , opt => opt.Ignore()) */
            .ForMember(dst => dst.invDetails, opt => opt.MapFrom(src => src.InvDetail));

            CreateMap<InvoiceRes, Invoice>()
              .ForMember(dst => dst.Id, opt => opt.Ignore())
              .ForMember(dst => dst.InvDetail, opt => opt.Ignore())
              .AfterMap((phr, ph) => AfterMapPurchase(phr, ph)  /* (phr,p                 h)=> {

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
               } */);


            CreateMap<InvDetail, InvDetailsRes>();
            CreateMap<InvDetailsRes, InvDetail>();

            CreateMap<JournalEntry, JournalEntryRes>()
                //.ForMember(dst => dst.Id , opt => opt.Ignore())
                .ForMember(dst => dst.journalAccounts, opt => opt.MapFrom(src => src.journalAccounts));

            CreateMap<JournalEntryRes, JournalEntry>()
            .ForMember(dst => dst.Id , opt => opt.Ignore())
            .ForMember(dst => dst.journalAccounts , opt => opt.Ignore())
            .AfterMap((jer, je) => AfterMapJournal(jer, je));

            CreateMap<JournalAccount, JournalAccountRes>();
            CreateMap<JournalAccountRes, JournalAccount>();





        }

        private static void AfterMapPurchase(InvoiceRes phr, Invoice ph)
        {

            // removing deleting items

            var removedItems = new List<InvDetail>();
            foreach (var item in ph.InvDetail)
                if (phr.invDetails.SingleOrDefault(pd => pd.Id == item.Id) == null)
                    removedItems.Add(item);
            foreach (var item in removedItems)
                ph.InvDetail.Remove(item);


            // updated changing
            foreach (var pdd in phr.invDetails)
            {
                if (!(pdd.Id > 0)) //New <0
                {
                    ph.InvDetail.Add(new InvDetail
                    {
                        Price = pdd.Price,
                        Quantity = pdd.Quantity,
                        ProductId = pdd.ProductId,
                        LastUpdate = DateTime.Now,
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

        }
        private static void AfterMapJournal(JournalEntryRes jer, JournalEntry je)
        {

            // removing deleting items

            var removedItems = new List<JournalAccount>();
            foreach (var item in je.journalAccounts)
                if (jer.journalAccounts.SingleOrDefault(pd => pd.Id == item.Id) == null)
                    removedItems.Add(item);
            foreach (var item in removedItems)
                je.journalAccounts.Remove(item);


            // updated changing
            foreach (var pdd in jer.journalAccounts)
            {
                if (!(pdd.Id > 0)) // not > 0 meaning new
                {
                      if (pdd.AppUserId > 0)
                            pdd.AccountId = (int)AccountType.Client ;
                    je.journalAccounts.Add(new JournalAccount
                    {
                        Credit = pdd.Credit,
                        Debit = pdd.Debit,
                       AccountId = pdd.AccountId,
                        AppUserId = pdd.AppUserId,
                        RefNo = pdd.RefNo,
                        DueDate = pdd.DueDate,
                    }
                     );
                }
                else // Updating
                {
                    var pd = je.journalAccounts.SingleOrDefault(p => p.Id == pdd.Id);
                    if (pd != null)
                    {
                        if (pd.Credit != pdd.Credit) pd.Credit = pdd.Credit;
                        if (pd.Debit != pdd.Debit) pd.Debit = pdd.Debit;
                        if (pd.AccountId != pdd.AccountId) pd.AccountId = pdd.AccountId;
                        if (pdd.AppUserId > 0) pd.AccountId = (int)AccountType.Client ;
                        if (pd.AppUserId != pdd.AppUserId) pd.AppUserId = pdd.AppUserId;
                        if (pd.RefNo != pdd.RefNo) pd.RefNo = pdd.RefNo;
                        if (pd.DueDate != pdd.DueDate) pd.DueDate = pdd.DueDate;
                    }

                }
            }

        }

        private void Checking(InvoiceRes InvRes){



        }

    }

}