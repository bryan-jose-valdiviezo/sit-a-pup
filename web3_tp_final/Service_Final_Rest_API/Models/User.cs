using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class User
    {
        public User()
        {
            Availabilities = new HashSet<Availability>();
            Messages = new HashSet<Message>();
            Pets = new HashSet<Pet>();
            Reviews = new HashSet<Review>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Availability> Availabilities { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
