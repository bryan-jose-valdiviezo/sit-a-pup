using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public class PhoneNumber
    {
        [Required]
        public int PhoneNum{ get; set; }

        [Required]
        public Type Type {get; set;}

        [Required]
        public int UserId { get; set; }
    }
}
