using System;
using System.Runtime.Serialization;


namespace ALBAB.Entities.Invoices
{

    public class InvoiceNoMask {


    enum Colors {Red = 1, Blue = 2};
    enum Mask
        {
         // quotation
        BQ,
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

    public static string GetMask() {
        Enum myColors = Mask.BQ;
        return myColors.ToString() + DateTime.Now.Year + "-0" ;
    }
}




}
