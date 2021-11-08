using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public enum Specie
    {
        CHIEN,
        CHAT,
        OISEAU,
        RONGEUR,
        REPTILE,
        FISH,
        AUTRE
    }

    public class Pet
    {
        public int PetID { get; set; }

        [Required(ErrorMessage = "Maximum 40 caractères")]
        [MaxLength(40)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Vous devez indiquer l'espèce")]
        public Specie Specie { get; set; } 
        
        public int BirthYear { get; set; }

        public string PhotoURI { get; set; }

        public bool IsBeingSitted { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        //Je n'ajoute pas d'ID dans l'attribut parce que ça compliquerait inutilement la gestion des clés primaires et étrangères avec le framework (CM).
        public int Sitter { get; set; }

        public DateTime SittingStart { get; set; }

        public DateTime SittingEnd { get; set; }

        public int UserID { get; set; }
    }
}
