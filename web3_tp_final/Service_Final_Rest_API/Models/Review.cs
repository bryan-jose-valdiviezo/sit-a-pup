using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Review
    {
        public long ReviewId { get; set; }
        public long Stars { get; set; }
        public long AppointmentId { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual User User { get; set; }
    }
}
