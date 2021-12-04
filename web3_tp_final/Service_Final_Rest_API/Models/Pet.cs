using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Pet
    {
        public Pet()
        {
            PetAppointments = new HashSet<PetAppointment>();
        }

        public long PetId { get; set; }
        public long BirthYear { get; set; }
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public string Specie { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PetAppointment> PetAppointments { get; set; }
    }
}
