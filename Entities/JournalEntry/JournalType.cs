using System;
using System.Runtime.Serialization;

namespace ALBAB.Entities.JournalEntry
{
    public enum JournalType
        {
         //[EnumMember(Value ="Receipt")]
        RECPT,
        //[EnumMember(Value="SalesReceipt")]
        SLRCT,
        //[EnumMember(Value="Paymnet")]
        PYMNT,
        //[EnumMember(Value="Purchase2Pay")]
        PR2PY,
        //[EnumMember(Value="Journal")]
        JORNL,
        //[EnumMember(Value="Purchase")]
        PURCH,
        //[EnumMember(Value="Sales")]
        SALES,
        QUOTE,

        }
}
