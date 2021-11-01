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
        [Required]
        [MinLength(3),MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();

        public List<Availability> Availabilities { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Message> Messages { get; set; }
    }
}
