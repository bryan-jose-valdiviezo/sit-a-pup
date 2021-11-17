using System;
using System.Collections.Generic;

#nullable disable

namespace Service_Final_Rest_API.Models
{
    public partial class Admin
    {
        public long AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
