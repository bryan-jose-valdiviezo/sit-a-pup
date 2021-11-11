using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web3_tp_final.Models
{
    public class Pet
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
        public Species Specie { get; set; }

        public int BirthYear { get; set; }

        public byte[] Photo { get; set; }

        public bool IsBeingSitted { get; set; }

        //Je n'ajoute pas d'ID dans l'attribut parce que ça compliquerait inutilement la gestion des clés primaires et étrangères avec le framework (CM).
        public int Sitter { get; set; }

        public DateTime SittingStart { get; set; }

        public DateTime SittingEnd { get; set; }

        public int UserID { get; set; }
    }
}
