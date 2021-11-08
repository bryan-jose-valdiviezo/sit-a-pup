using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public class User
    {
        public int UserID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Minimum 3 caractères, maximum 20 caractères")]
        [MinLength(3),MaxLength(20)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Entrez une adresse valide (ex: nom@courriel.com)")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vous devez entrer une adresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vous devez entrer un numéro de téléphone")]
        //[RegularExpression(@"\d{3}[-]\d{3}[-]\d{4}")]
        public string PhoneNumber { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();

        public List<Availability> Availabilities { get; set; } = new List<Availability>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
