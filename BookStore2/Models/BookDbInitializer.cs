using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class BookDbInitializer : CreateDatabaseIfNotExists<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Война и мир", Author = "Л. Толстой"
                , Price = 220, Description = "Description", ImagePath = "Images/War and Peace.png" });
            db.Books.Add(new Book { Name = "Отцы и дети", Author = "И. Тургенев"
                , Price = 180, Description = "Description", ImagePath = "Images/Fathers and Sons.png" });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов"
                , Price = 150, Description = "Description", ImagePath = "Images/The Sea Gull.png" });

            db.Users.Add(new User { Login = "Admin", Pass = "admin", IsAdmin = true });

            base.Seed(db);
        }
    }
}