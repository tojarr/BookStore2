using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class BookViewModel
    {
        // ID книги
        public int Id { get; set; }
        // название книги
        [Required]
        [DisplayName("Название")]
        public string Name { get; set; }
        // автор книги
        [Required]
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
        //Quantity
        [DisplayName("Кол-во на складе")]
        public int QuantityInStorage { get; set; }
    }
}