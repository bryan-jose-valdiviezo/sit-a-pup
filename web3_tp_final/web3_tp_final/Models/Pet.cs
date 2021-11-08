using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace web3_tp_final.Models
{
    public class Pet
    {
        public Pet()
        {
            SpecieList = Enum.GetNames(typeof(Specie)).Select(name => new SelectListItem()
            {
                Text = name,
                Value = name
            });
        }

        public int PetID { get; set; }

        [Required(ErrorMessage = "Maximum 40 caractères")]
        [MaxLength(40)]
        public string Name { get; set; }

        public Specie Specie { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> SpecieList { get; set; }

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
