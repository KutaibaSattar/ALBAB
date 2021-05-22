using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;

namespace ALBAB.Entities.Purchases
{
    public class Invoice : BaseEntity
    {

        [Required]
        public string Type { get; set; } = "PR";
        public string InvNo { get; set; }
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }

        public dbAccounts Account { get; set; }
        [Required]
      private int _accountId;
        public int AccountId { // if AppUserId is not null then AccountId = 30(Clients)

             get => _accountId;

              set => _accountId = (AppUserId == null) ? value : (int)(ReservedAccountsType.Clients); }

        public ICollection <InvDetail> InvDetail { get; set; }
         public Invoice()
        {
               InvDetail = new Collection<InvDetail>();
        }


    }
}