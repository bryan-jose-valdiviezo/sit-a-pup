using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class PetAppointment
    {
        public long PetAppointmentId { get; set; }
        public long PetId { get; set; }
        public long AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
