using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
