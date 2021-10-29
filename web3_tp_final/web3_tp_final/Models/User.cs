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
        public String Address { get; set; }

        public List<PhoneNumber> PhoneNumbers { get; set; }

        public List<Pet> OwnedPets { get; set; }

        public List<Pet> SittedPets { get; set; }

        public List<Availability> Availabilities { get; set; }

        public List<Review> MyReviews { get; set; }

        public List<Review> ReviewsOtherUsers { get; set; }

        public List<Message> MessagesSent { get; set; }
    }
}
