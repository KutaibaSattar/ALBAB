using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.JournalEntry;

namespace ALBAB.Entities.Invoices
{
    public class SaveInvRes

    {
        [RequiredGreaterThanZero]
        public int? Id { get; set; }
        [RegularExpression(@"[A-Z]{2}[0-9\-]{5}(0|[1-9][0-9]*)$",
         ErrorMessage = "ListNo should formated [BS2020-1].")]
        public string InvNo { get; set; }
        [Required]
        public JournalType Type { get; set; }
        public InvStatusType Status { get; set; }
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }
        public int? AddressId { get; set; }
        public int? AppUserId { get; set; }

        // private int? _acctionAccont = (int)AccountType.CostGoodsSold ;
        // [Required]
        // public int? ActionAcctId { // if 0 then set null and update accountId in JournalAccount entity

        //       get => _acctionAccont;
        //       set => _acctionAccont = (value ==0 || value==null ) ? _acctionAccont : value ;
        //      }

        //public int? DebitAccountId {get;set;}  = (int)AccountType.CostGoodsSold;

        public int? ActionAcctId { get; set; }
        public decimal?  Discount { get; set; }

        // private int? _vatAccount = (int)AccountType.Vat ;



        // public int? VatAcctId { // if 0 then set null and update accountId in JournalAccount entity

        //       get => _vatAccount;
        //       set => _vatAccount = (value ==0 || value==null ) ? _vatAccount : value ;
        //      }
        //public int? VatAccountId  {get;set;} = (int)AccountType.Vat;

       public int? VatAcctId { get; set; }
        public decimal?  Vat { get; set; }
        public decimal?  TotalAmount { get; set; }
        public int dbAccountId{ get; set; }
         public string Description { get; set; }
         public ICollection <InvDetailsRes> invDetails { get; set; }
         public SaveInvRes()
        {
               invDetails = new Collection<InvDetailsRes>();
        }



    }
}