using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class BookViewModel
    {
        // ID книги
        public int Id { get; set; }
        // название книги
        [DisplayName("Название")]
        public string Name { get; set; }
        // автор книги
        [DisplayName("Автор")]
        public string Author { get; set; }
        // цена
        [DisplayName("Цена")]
        public int Price { get; set; }
        //Description
        [DisplayName("Описание")]
        public string Description { get; set; }
        //Image
        [DisplayName("Путь к картинке")]
        public string ImagePath { get; set; }
    }
}