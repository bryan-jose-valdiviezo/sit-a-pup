
using System.ComponentModel.DataAnnotations;

namespace web3_tp_final.Models
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
