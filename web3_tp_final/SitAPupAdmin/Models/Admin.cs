using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        [Required(ErrorMessage = "Minimum 3 caractères, maximum 20 caractères")]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }
        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Minimum 6 caractères, maximum 32 caractères")]
        [MinLength(3), MaxLength(20)]
        public string Password { get; set; }
    }
}
