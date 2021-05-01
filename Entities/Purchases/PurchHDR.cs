using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Purchases
{
    public class PurchHdr : BaseEntity
    {
       
        public string purNo { get; set; }
        public DateTime purDate { get; set; }
        public string purComment { get; set; }
        public DateTime LastUpdate { get; set; }
        public AppUser AppUser { get; set; }
        
        [Required]
        public int AppUserId { get; set; }
        public ICollection <PurchDtl> purchDtl { get; set; }
         public PurchHdr()
        {
               purchDtl = new Collection<PurchDtl>();
        }
         

    }
}