using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.Invoices;

namespace ALBAB.Entities
{
    public class RequiredGreaterThanZero : ValidationAttribute
    {
                protected override ValidationResult  IsValid(object value, ValidationContext validationContext)
            {
                // return true if value is a non-null number > 0, otherwise return false
                int i;

                if(value == null || int.TryParse(value.ToString(), out i) && i > 0)
                     return ValidationResult.Success;
                return new ValidationResult("Id value should be greater than zero ");
            }

        //     protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        // {

        //     var customer = (Customer)validationContext.ObjectInstance;


        //    if (customer.MemberShipTypeId == MemberShipType.Unknown ||  customer.MemberShipTypeId == MemberShipType.PayAsYouGo)
        //         return ValidationResult.Success;

        //     if (customer.Birethday == null)
        //         return new ValidationResult("Birthday is required. ");

        //     var age = DateTime.Today.Year - customer.Birethday.Value.Year;

        //     return (age >= 18)
        //         ? ValidationResult.Success
        //         : new ValidationResult("Customer should be at least 18 years old to");


        // }

    }
}