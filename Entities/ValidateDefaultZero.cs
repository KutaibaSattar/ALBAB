using System.ComponentModel.DataAnnotations;
using ALBAB.Entities.Purchases;

namespace ALBAB.Entities
{
    public class ValidateDefaultZero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var inv =  validationContext.ObjectInstance;
            return new ValidationResult("Hello");

        }
    }
}