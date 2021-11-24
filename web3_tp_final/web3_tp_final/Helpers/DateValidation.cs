using System;
using System.ComponentModel.DataAnnotations;

namespace web3_tp_final.Helpers
{
    public class DateValidation: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((DateTime)value >= DateTime.Today)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("La date ne peut pas être dans le passé.");
            }
        }
    }
}
