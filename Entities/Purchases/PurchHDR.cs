using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Purchases
{
    public class PurchHDR : BaseEntity
    {
       
        public string purNo { get; set; }
        public DateTime purDate { get; set; }
        public string purComment { get; set; }
        public DateTime? LastUpdate { get; set; }
        public AppUser AppUser { get; set; }
         public int AppUserId { get; set; }
        public ICollection <PurchDTL> purchDTL { get; set; }
         public PurchHDR()
        {
               purchDTL = new Collection<PurchDTL>();
        }

       

    }
}