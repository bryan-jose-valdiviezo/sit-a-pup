using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Models;

namespace web3_tp_final.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }

        public int OwnerId { get; set; }

        public int SitterId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<int> PetIds { get; set; }
    }
}
