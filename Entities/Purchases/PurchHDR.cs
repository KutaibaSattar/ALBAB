using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBAB.Entities.Purchases
{
    public class Invoice : BaseEntity
    {

        [Required]
        public string Type { get; set; } = "PRCH";
        public string InvNo { get; set; }
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }
        public DateTime LastUpdate { get; set; }
        public dbAccounts Account { get; set; }
         public int AccountId {get;set;}
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        [Required]
        public int DebitAccountId { get; set;}
       
        [Column(TypeName = "decimal(5, 2)")]
        public decimal?  SubTotal { get; set; }
       [Column(TypeName = "decimal(5, 2)")]
        public decimal?  Discount { get; set; }
        public int  VatAccountId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal?  Vat { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal?  TotalAmount { get; set; }

        public ICollection <InvDetail> InvDetail { get; set; }
         public Invoice()
        {
               InvDetail = new Collection<InvDetail>();
        }


    }
}