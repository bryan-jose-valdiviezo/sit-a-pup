using System;
using System.ComponentModel.DataAnnotations;

namespace web3_tp_final.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
    }
}
