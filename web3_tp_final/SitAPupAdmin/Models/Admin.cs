using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitAPupAdmin.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
       
        public string Name { get; set; }
       
        public string Password { get; set; }
    }
}
