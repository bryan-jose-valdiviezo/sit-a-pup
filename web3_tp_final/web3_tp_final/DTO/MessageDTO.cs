using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.DTO
{
    public class MessageDTO
    {

        public int MessageID { get; set; }

        
        public string Content { get; set; }

      
        public DateTime TimeStamp { get; set; }

      
        public int Sender { get; set; }

       
        public int Recipient { get; set; }
    }
}
