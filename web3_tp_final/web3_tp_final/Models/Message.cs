using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        
        [Required]
        public string Content{get;set;}

        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public int Sender { get; set; }

        [Required]
        public int Recipient { get; set; }
    }
}
