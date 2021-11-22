using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web3_tp_final.Helpers;

namespace web3_tp_final.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }

        //Required est implicite
        [DateValidation] //Classe custom dans Helpers
        public DateTime StartDate { get; set; }

        //Required est implicite
        [DateValidation] //Classe custom dans Helpers
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
    }
}
