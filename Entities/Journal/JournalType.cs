using System.Runtime.Serialization;

namespace ALBAB.Entities.Journal
{
    public enum JournalType
        {
         [EnumMember(Value="Receipt")]
        RCPT,
        [EnumMember(Value="SalesReceipt")]
        SLRT,
        [EnumMember(Value="Paymnet")]
        PYMT,
        [EnumMember(Value="Purchase2Pay")]
        PR2P,
        [EnumMember(Value="Journal")]
        JRNL,
        [EnumMember(Value="Purchase")]
        PRCH,
        [EnumMember(Value="Sales")]
        SALS,
        }
}
