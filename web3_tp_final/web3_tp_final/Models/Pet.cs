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
        public int PetID { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        
        [Required]
        public Specie Specie { get; set; } 
        
        public int Age { get; set; }

        public string PhotoURI { get; set; }

        public User owner { get; set; }
    }
}
