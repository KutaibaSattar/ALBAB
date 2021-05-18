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
        public DateTime Date { get; set; } =  DateTime.Now;
        public string Comment { get; set; }

        [Required]
        public int AppUserId { get; set; }
        public int AccountId { get; set; }
         public ICollection <InvDetailsRes> invDetails { get; set; }
         public InvoiceRes()
        {
               invDetails = new Collection<InvDetailsRes>();
        }



    }
}