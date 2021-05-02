using System.Runtime.Serialization;

namespace ALBAB.Entities.Journal
{
   public enum JournalType 
    {

        [EnumMember(Value="Receipt Voucher")]
        RTV,
        
        [EnumMember(Value="Payment Voucher")]
        PYV,
       
        [EnumMember(Value="Journal Voucher")]
        JRV,
       
           
      

        /* (i) Receipt Voucher
        (ii) Payment Voucher
        (iii) Non-Cash or Transfer Voucher or Journal Voucher
        (iv) Supporting Voucher */

    }
}


