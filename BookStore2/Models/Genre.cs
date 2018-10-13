using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class Genre:BaseEntity
    {
        public int IdGenre { get; set; }
        public string GenreBook { get; set; }
    }
}