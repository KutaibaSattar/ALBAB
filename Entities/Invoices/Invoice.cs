using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ALBAB.Entities.JournalEntry;

namespace ALBAB.Entities.Invoices
{

    //[Index(nameof(InvNo), nameof(Type), IsUnique = true)]
    public class Invoice : BaseEntity
    {

        [Required]
        [MaxLength(20)]
        public string InvNo { get; set; }
        [Required]
        [MaxLength(10)]
        public JournalType Type { get; set; }
        [MaxLength(10)]
        public InvStatusType Status {get; set;}
        public DateTime Date { get; set; }
        public DateTime LastUpdate { get; set; }

        public string Comment { get; set; }
        public AppUser AppUser { get; set; }
        public int? AppUserId { get; set; }
        public dbAccount dbAccount { get; set; }
        public int dbAccountId {get;set;}
        public int ActionAcctId { get; set;}

        [Column(TypeName = "decimal(10, 3)")]
        public decimal?  SubTotal { get; set; }
       [Column(TypeName = "decimal(10, 3)")]
        public decimal?  Discount { get; set; }
        public int  VatAcctId { get; set; }
        [Column(TypeName = "decimal(10, 3)")]
        public decimal?  Vat { get; set; }
        [Column(TypeName = "decimal(10, 3)")]

        public decimal?  TotalAmount {
            get { return InvDetail.Sum(v => v.TotalValue);}
             }
        public decimal?  InvCost {
            get { return InvDetail.Sum(v => (v.Quantity * v.Cost));}
        }

        public ICollection <InvDetail> InvDetail { get; set; }
         public Invoice()
        {
               InvDetail = new Collection<InvDetail>();
        }


    }
}