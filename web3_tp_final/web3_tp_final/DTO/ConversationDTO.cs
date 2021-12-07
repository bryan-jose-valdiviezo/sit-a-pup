using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Models;

namespace web3_tp_final.DTO
{
    public class ConversationDTO
    {
        public int UserID { get; set; }

        public int RecipientID { get; set; }

        public IEnumerable<Message> Conversation { get; set; }
    }
}
