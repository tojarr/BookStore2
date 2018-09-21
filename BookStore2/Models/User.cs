﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", 
            ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public string Pass { get; set; }
        public bool IsAdmin { get; set; }

    }
}