using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int Stars { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
