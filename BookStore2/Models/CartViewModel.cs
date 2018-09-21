using BookStore2.Serveces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BookStore2.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }


        public void AddItem(Book book, int quantity)//добавление в корзину продукта определенного количества
        {
            CartLine line = lineCollection.Where(p => p.Book.Id == book.Id).FirstOrDefault();
            
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                }
                    );
            }
            else
            {
                if (line.Quantity < book.QuantityInStorage)
                {
                    line.Quantity += quantity;
                }
                else
                {
                    //нехватает книг на складе
                }   
            }
        }

        public void RemoveLine(Book book)
        {
            lineCollection.RemoveAll(l => l.Book.Id == book.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Book.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public List<CartLine> SendMail(string login, IQueryable<Book> dataBase, IEmailSender _emailSender)
        {
            decimal sum = ComputeTotalValue();
            string shopList = "<tr><td>" +
                "Автор</td><td>" +
                "Название</td><td>" +
                "Количество</td><td>" +
                "Цена</td><td>" +
                "Сумма" +
                "</td></tr>";

            //BookContext db = dataBase;

            foreach (var line in lineCollection)
            {
                Book book = dataBase.FirstOrDefault(b => b.Id == line.Book.Id);
                if (line.Quantity > book.QuantityInStorage)
                {
                    return null;
                }
                shopList += "<tr><td>" + line.Book.Author + "</td><td>"
                    + line.Book.Name + "</td><td align = \"right\">"
                    + line.Quantity + "</td><td align = \"right\">"
                    + line.Book.Price + "</td><td align = \"right\">" +
                    (line.Quantity * line.Book.Price) + "</td></tr>";
            }
            string bodyMail = "<h2>Список ваших покупок.</h2>" + "<div><table class = 'table'>" + shopList + "</table></div>" +
                "<div><p>" + login + " сумма вашей покупки - " + sum + "</p>" +
                "<p>Спасибо за покупку!</p>" + "</div>";
            string subjMail = "Покупка в книжном магазине";


            // вызов метода отправки на почту
            _emailSender.SendEmail(login, subjMail, bodyMail);

            return lineCollection;
        }
    }

    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
