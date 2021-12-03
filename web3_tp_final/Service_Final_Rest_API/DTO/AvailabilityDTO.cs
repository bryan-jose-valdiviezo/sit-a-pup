using Service_Final_Rest_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service_Final_Rest_API.DTO
{
    public class AvailabilityDTO
    {
        public int AvailabilityID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long UserId { get; set; }
    }
}
