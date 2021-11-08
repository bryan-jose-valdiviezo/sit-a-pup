using System;
using System.ComponentModel.DataAnnotations;

namespace web3_tp_final.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        [Required]
        public int Stars { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public int WrittenTo { get; set; }

        [Required]
        public int WrittenBy { get; set; }
    }
}
