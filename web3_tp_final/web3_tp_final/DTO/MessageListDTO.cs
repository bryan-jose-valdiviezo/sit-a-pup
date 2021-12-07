using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Models;

namespace web3_tp_final.DTO
{
    public class MessageListDTO
    {
        Dictionary<string, Message> LastMessage { get; set; }
    }
}
