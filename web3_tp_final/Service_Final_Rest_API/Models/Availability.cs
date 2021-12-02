using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Availability
    {
        public long AvailabilityId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
