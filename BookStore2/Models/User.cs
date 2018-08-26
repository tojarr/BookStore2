using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public bool IsAdmin { get; set; }

    }
}