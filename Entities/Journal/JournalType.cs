using System.Runtime.Serialization;

namespace ALBAB.Entities.Journal
{
   public static class JournalType
    {

        public static string Receipt { get { return "RT";} }
        public static string Payment { get { return "PT";} }
        public static string Journal { get { return "JL";} }
        public static string Purchase { get { return "PR";} }
        public static string Sales { get { return "SL";} }

        /* (i) Receipt Voucher
        (ii) Payment Voucher
        (iii) Non-Cash or Transfer Voucher or Journal Voucher
        (iv) Supporting Voucher */

    }
}
