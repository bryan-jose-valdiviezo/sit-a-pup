using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Review
    {
        public long ReviewId { get; set; }
        public long Stars { get; set; }
        public string Comment { get; set; }
        public string TimeStamp { get; set; }
        public long WrittenTo { get; set; }
        public long WrittenBy { get; set; }
        public long? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
