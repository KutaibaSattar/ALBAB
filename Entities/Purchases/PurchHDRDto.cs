using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Purchases
{
    public class InvoiceRes

    {
        public int? Id { get; set; }
        public string InvNo { get; set; }
        public string Type { get; set; } = "PRCH";
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }
        public int? AppUserId { get; set; }


        private int? _debitAccount = (int)AccountType.CostGoodsSold ;
        [Required]
        public int? DebitAcctId { // if 0 then set null and update accountId in JournalAccount entity

              get => _debitAccount;
              set => _debitAccount = (value ==0 || value==null ) ? _debitAccount : value ;
             }

        //public int? DebitAccountId {get;set;}  = (int)AccountType.CostGoodsSold;
        public decimal?  Discount { get; set; }

        private int? _vatAccount = (int)AccountType.Vat ;

        public int? VatAcctId { // if 0 then set null and update accountId in JournalAccount entity

              get => _vatAccount;
              set => _vatAccount = (value ==0 || value==null ) ? _vatAccount : value ;
             }
        //public int? VatAccountId  {get;set;} = (int)AccountType.Vat;
        public decimal?  Vat { get; set; }
        public decimal?  TotalAmount { get; set; }
        public int AccountId{ get; set; }

         public ICollection <InvDetailsRes> invDetails { get; set; }
         public InvoiceRes()
        {
               invDetails = new Collection<InvDetailsRes>();
        }



    }
}