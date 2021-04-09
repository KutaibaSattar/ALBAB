using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ALBAB.Entities.AppAccounts;

namespace ALBAB.Entities.Purchases
{
    public class SavePurchHdrDto : BaseEntity 
    {
        public string purNo { get; set; }
        public DateTime purDate { get; set; }
        public string purComment { get; set; }
       [Required]
        public int AppUserId { get; set; }
         public ICollection <PurchDtlDto> purchDtlDtos { get; set; }
         public SavePurchHdrDto()
        {
               purchDtlDtos = new Collection<PurchDtlDto>();
        }
        
    }
}