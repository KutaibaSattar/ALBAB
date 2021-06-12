using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ALBAB.Entities.Purchases
{
    public class Invoice : BaseEntity
    {

        [Required]
        public string InvNo { get; set; }
        public string Type { get; set; } = "PRCH";
        public DateTime Date { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Comment { get; set; }
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public dbAccounts Account { get; set; }
        public int AccountId {get;set;}
        public int DebitAcctId { get; set;}

        [Column(TypeName = "decimal(5, 2)")]
        public decimal?  SubTotal { get; set; }
       [Column(TypeName = "decimal(5, 2)")]
        public decimal?  Discount { get; set; }
        public int  VatAcctId { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal?  Vat { get; set; }
        [Column(TypeName = "decimal(5, 2)")]

        public decimal?  TotalAmount {
            get { return InvDetail.Sum(v => v.Value);}
             }

        public ICollection <InvDetail> InvDetail { get; set; }
         public Invoice()
        {
               InvDetail = new Collection<InvDetail>();
        }


    }
}