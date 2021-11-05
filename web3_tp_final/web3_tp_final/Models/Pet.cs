using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public enum Specie
    {
        DOG,
        CAT,
        BIRD,
        RODENT,
        AMPHIBIAN,
        REPTILE,
        FISH,
        EXOTIC
    }

    public class Pet
    {
        [Required]
        public int PetID { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        
        [Required]
        public Specie Specie { get; set; } 
        
        public int Age { get; set; }

        public string PhotoURI { get; set; }

        public bool IsBeingSitted { get; set; }

        public int Sitter { get; set; }

        public DateTime SittingStart { get; set; }

        public DateTime SittingEnd { get; set; }

        public int UserID { get; set; }
    }
}
