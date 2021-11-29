using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web3_tp_final.Models
{
    public class Pet: IValidatableObject
    {
        public enum Species
        {
            CHIEN,
            CHAT,
            OISEAU,
            RONGEUR,
            REPTILE,
            POISSON,
            AUTRE
        }

        public int PetID { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Maximum 40 caractères")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Column("Specie")]
        public string SpecieString
        {
            get { return Specie.ToString(); }
            private set { Specie = (Species)Enum.Parse(typeof(Species), value, true); }
        }

        [NotMapped]
        [Display(Name = "Espèce")]
        public Species Specie { get; set; }

        [Display(Name = "Année de naissance")]
        [Required(ErrorMessage = "Année de naissance requise")]
        public int BirthYear { get; set; }

        public byte[] Photo { get; set; }

        public int UserID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthYear > DateTime.Now.Year)
            {
                yield return new ValidationResult("L'année de naissance ne peut être dans le futur");
            } else if (BirthYear < DateTime.Now.Year - 30)
            {
                yield return new ValidationResult("L'année de naissance ne peut être plus de 30 ans dans le passé");
            }
        }
    }
}
