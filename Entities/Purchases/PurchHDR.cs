using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

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
        [Required]
        public int? AppUserId { get; set; }
        public dbAccounts Account { get; set; }
        public int AccountId { get; set; }
        public ICollection <InvDetail> InvDetail { get; set; }
         public Invoice()
        {
               InvDetail = new Collection<InvDetail>();
        }


    }
}