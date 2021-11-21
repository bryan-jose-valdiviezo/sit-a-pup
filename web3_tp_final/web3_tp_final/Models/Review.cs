﻿using System;
using System.ComponentModel.DataAnnotations;

namespace web3_tp_final.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        //[Required]
        public int Stars { get; set; }

        //[Required]
        public string Comment { get; set; }

        //[Required]
        public Appointment Appointment { get; set; }

        //[Required]
        public User User { get; set; }
    }
}
