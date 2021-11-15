using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Pet
    {
        public Pet()
        {
            Reviews = new HashSet<Review>();
        }

        public long PetId { get; set; }
        public string Name { get; set; }
        public long Specie { get; set; }
        public long BirthYear { get; set; }
        public string PhotoUri { get; set; }
        public long IsBeingSitted { get; set; }
        public long Sitter { get; set; }
        public string SittingStart { get; set; }
        public string SittingEnd { get; set; }
        public long UserId { get; set; }
        public long? Photo { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
