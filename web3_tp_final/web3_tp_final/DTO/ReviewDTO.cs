using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.DTO
{
    public class ReviewDTO
    {
        public int ReviewID { get; set; }

        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        //[Required]
        public int Stars { get; set; }

        //[Required]
        public string Comment { get; set; }
    }
}
