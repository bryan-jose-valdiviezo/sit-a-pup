using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            PetAppointments = new HashSet<PetAppointment>();
            Reviews = new HashSet<Review>();
        }

        public long AppointmentId { get; set; }
        public long SitterId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? OwnerId { get; set; }
        public long IsActive { get; set; }
        public string Status { get; set; }

        public virtual User Owner { get; set; }
        public virtual User Sitter { get; set; }
        public virtual ICollection<PetAppointment> PetAppointments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
