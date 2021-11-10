using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Message
    {
        public long MessageId { get; set; }
        public string Content { get; set; }
        public string TimeStamp { get; set; }
        public long Sender { get; set; }
        public long Recipient { get; set; }
        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
