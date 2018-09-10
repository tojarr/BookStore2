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

        public List<CartLine> SendMail(string login, BookContext dataBase)
        {
            decimal sum = ComputeTotalValue();
            string shopList = "<tr><td>" +
                "Автор</td><td>" +
                "Название</td><td>" +
                "Количество</td><td>" +
                "Цена</td><td>" +
                "Сумма" +
                "</td></tr>";
            
            BookContext db = dataBase;

            foreach (var line in lineCollection)
            {
                Book book = db.Books.Find(line.Book.Id);
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

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("matroskin0711test@gmail.com", "bookstore");
            // кому отправляем
            MailAddress to = new MailAddress(login);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Покупка в книжном магазине";
            // текст письма
            m.Body = "<h2>Список ваших покупок.</h2>" + "<div><table class = 'table'>" + shopList + "</table></div>" +
                "<div><p>" + login + " сумма вашей покупки - " + sum + "</p>" +
                "<p>Спасибо за покупку!</p>" + "</div>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("matroskin0711test@gmail.com", "LeOsEo623487");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return lineCollection;
        }
    }

    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
