using System.ComponentModel.DataAnnotations;

namespace FeatureManagementAPI
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateTime date))
            {
                return ValidationResult.Success;
            }

            if (date < DateTime.Now.Date)
            {
                return new ValidationResult("The date must be a future date.");
            }

            return ValidationResult.Success;
        }
    }
}
