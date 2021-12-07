using System;
using System.ComponentModel.DataAnnotations;

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
