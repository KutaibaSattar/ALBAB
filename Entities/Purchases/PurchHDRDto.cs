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
        public string Type { get; set; } = "PR";
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }

        private int? _userId ;
        public int? AppUserId {

              get => _userId;
              set => _userId = (value ==0 ) ? null : value ;
             }

        public int AccountId {get;set;}

        /* private int _accountId;
        public int AccountId {

             get => _accountId;

              set => _accountId = (_userId == null) ? value : (int)(ReservedAccountsType.Clients); } */

         public ICollection <InvDetailsRes> invDetails { get; set; }
         public InvoiceRes()
        {
               invDetails = new Collection<InvDetailsRes>();
        }



    }
}