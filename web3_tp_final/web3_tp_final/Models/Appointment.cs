﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }

        public User Sitter { get; set; }

        public User Owner { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Pet> Pets { get; set; }
    }
}