using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using web3_tp_final.Helpers;

namespace web3_tp_final.Models
{
    public class Availability: IValidatableObject
    {
        public int AvailabilityID { get; set; }

        //Required est implicite
        [DateValidation] //Classe custom dans Helpers
        public DateTime StartDate { get; set; }

        //Required est implicite
        [DateValidation] //Classe custom dans Helpers
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.Date > EndDate.Date)
            {
                yield return new ValidationResult("La date de fin ne peut être avant celle de début.");
            }
        }
    }
}
