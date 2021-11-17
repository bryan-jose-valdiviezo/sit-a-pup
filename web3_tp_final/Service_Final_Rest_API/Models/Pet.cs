using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Pet
    {
        public long PetId { get; set; }
        public string Name { get; set; }
        public string Specie { get; set; }
        public long BirthYear { get; set; }
        public byte[] Photo { get; set; }
        public long IsBeingSitted { get; set; }
        public long Sitter { get; set; }
        public string SittingStart { get; set; }
        public string SittingEnd { get; set; }
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
