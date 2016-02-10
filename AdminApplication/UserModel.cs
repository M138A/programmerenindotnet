using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class UserModel
    {
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string accountnumber { get; set; }
        public decimal balance { get; set; }

    }
}