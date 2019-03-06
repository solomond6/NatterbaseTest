using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Natterbase.Models
{
    public class Singup
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}