
using AutoMapper;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.Products;
using ALBAB.Entities.Invoices;
using System.Linq;
using System;
using System.Collections.Generic;
using ALBAB.Entities.JournalEntry;


namespace ALBAB.Entities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            //CreateMap<JournalType, string>().ConvertUsing(src => src.ToString());

            CreateMap<AppUser, RegisterRes>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser, AppUserRes>().ForMember(dto => dto.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();

            CreateMap<AppUser, MemberRes>()
                .ForMember(dst => dst.KeyId, opt => opt.MapFrom(src => src.UserName));

            CreateMap<AppUser,MemberUpdateRes>()
                .ForMember(dst => dst.KeyId, opt => opt.MapFrom(src => src.UserName)).ReverseMap();



            CreateMap<AddressRes,Address>().ReverseMap();
               //.ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<dbAccount, dbAccountsFlattenRes>();

            CreateMap<dbAccount, dbAccountsRes>().ReverseMap();

            CreateMap<Brand, BrandDto>();

            CreateMap<Model, ModelDto>();

            CreateMap<Product, ProductDto>()
            .ForMember(dst => dst.Brand, opt => opt.MapFrom(src => src.Model.Brand.Name))
            .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model.Name));

            CreateMap<Invoice, SaveInvRes>()
            // .ForMember(destination => destination.Type,
            //      opt => opt.MapFrom(source => Enum.GetName(typeof(JournalType), source.Type)))

                      /* .ForMember(dst => dst.Id , opt => opt.Ignore()) */
            .ForMember(dst => dst.invDetails, opt => opt.MapFrom(src => src.InvDetail));

            CreateMap<SaveInvRes, Invoice>()

              .ForMember(dst => dst.Id, opt => opt.Ignore())
              .ForMember(dst => dst.InvDetail, opt => opt.Ignore())
              .AfterMap((phr, ph) => AfterMapPurchase(phr, ph));




            CreateMap<InvDetail, InvDetailsRes>();
            CreateMap<InvDetailsRes, InvDetail>();

            CreateMap<Journal, JournalEntryRes>()
                //.ForMember(dst => dst.Id , opt => opt.Ignore())
                .ForMember(dst => dst.journalAccounts, opt => opt.MapFrom(src => src.journalAccounts));



             CreateMap<JournalAccount, AccountStatementRes>()
                  .ForMember(dst => dst.JENo, opt => opt.MapFrom(src => src.Journal.JENo))
                  .ForMember(dst => dst.Type, opt => opt.MapFrom(src => src.Journal.Type))
                  .ForMember(dst => dst.Note, opt => opt.MapFrom(src => src.Journal.Note))
                  .ForMember(dst => dst.EntryDate, opt => opt.MapFrom(src => src.Journal.EntryDate))
                  .ForMember(dst => dst.AccountName, opt => opt.MapFrom(src => src.dbAccount.Name))
                  .ForMember(dst => dst.SupplierName, opt => opt.MapFrom(src => src.AppUser.Name));






            CreateMap<JournalEntryRes, Journal>()
            .ForMember(dst => dst.Id , opt => opt.Ignore())
            .ForMember(dst => dst.journalAccounts , opt => opt.Ignore())
            .AfterMap((jer, je) => AfterMapJournal(jer, je));


            CreateMap<JournalAccount, JournalAccountRes>();
            CreateMap<JournalAccountRes, JournalAccount>();





        }

        private static void AfterMapPurchase(SaveInvRes phr, Invoice ph)
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
                        Cost = pdd.Cost,
                        Description = pdd.Description,
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
                        if (pd.Description != pdd.Description) pd.Description = pdd.Description;

                    }

                }
            }

        }
        private static void AfterMapJournal(JournalEntryRes jer, Journal je)
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
                       dbAccountId = pdd.AccountId,
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
                        if (pd.dbAccountId != pdd.AccountId) pd.dbAccountId = pdd.AccountId;
                        if (pdd.AppUserId > 0) pd.dbAccountId = (int)AccountType.Client ;
                        if (pd.AppUserId != pdd.AppUserId) pd.AppUserId = pdd.AppUserId;
                        if (pd.RefNo != pdd.RefNo) pd.RefNo = pdd.RefNo;
                        if (pd.DueDate != pdd.DueDate) pd.DueDate = pdd.DueDate;
                    }

                }
            }

        }
          private static void MapGrade(string grade)
        {
            //TODO: function to map a string to a SchoolGradeDTO

        }



    }


}