using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    public class BookDbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext db)
        {
            db.Entities.Add(new Genre { GenreBook ="Фантастика" });
            db.Entities.Add(new Genre { GenreBook = "Классика" });
            db.Entities.Add(new Genre { GenreBook = "Детектив" });

            db.Entities.Add(new Book
            {
                Name = "Война и мир",
                Genre = new Genre { GenreBook = "Классика" }.GenreBook,
                Author = "Л. Толстой"
                ,
                Price = 220,
                Description = "«Война́ и мир» (рус. дореф. «Война и миръ») — " +
                "роман-эпопея Льва Николаевича Толстого, описывающий русское общество в эпоху войн " +
                "против Наполеона в 1805—1812 годах.",
                ImagePath = "Images/War and Peace.png",
                QuantityInStorage = 3
            });
            db.Entities.Add(new Book
            {
                Name = "Отцы и дети",
                Genre = new Genre { GenreBook = "Классика" }.GenreBook,
                Author = "И. Тургенев"
                ,
                Price = 180,
                Description = "«Отцы́ и де́ти» (рус. дореф. Отцы и Дѣти) — роман русского писателя " +
                "Ивана Сергеевича Тургенева (1818—1883), написанный в 60 - е годы XIX века.",
                ImagePath = "Images/Fathers and Sons.png",
                QuantityInStorage = 3
            });
            db.Entities.Add(new Book
            {
                Name = "Чайка",
                Genre = new Genre { GenreBook = "Классика" }.GenreBook,
                Author = "А. Чехов"
                ,
                Price = 150,
                Description = "«Ча́йка» — пьеса в четырёх действиях Антона Павловича Чехова, " +
                "написанная в 1895—1896 годах и впервые опубликованная в журнале «Русская мысль»," +
                "в № 12 за 1896 год.Премьера состоялась 17 октября 1896 года на сцене" +
                " петербургского Александринского театра",
                ImagePath = "Images/The Sea Gull.png",
                QuantityInStorage = 0
            });
            db.Entities.Add(new Book
            {
                Name = "Звездные войны.Эпизод 7.",
                Genre = new Genre { GenreBook = "Фантастика" }.GenreBook,
                Author = "А. Д. Фостер"
                ,
                Price = 250,
                Description = "Звёздные войны: Пробуждение Силы (англ. Star Wars: The Force Awakens) " +
                "— новеллизация фильма «Звёздные войны.Эпизод VII: Пробуждение Силы». Роман, " +
                "автором которого выступил Алан Дин Фостер, выпущен издательством Del Rey " +
                "сперва в электронном варианте 18 декабря 2015 года, в один день с фильмом." +
                "Издание в твёрдой обложке появилось на прилавках 5 января 2016 года." +
                "На русском языке роман выпущен издательством «Азбука» 27 апреля 2016 года, " +
                "в переводе участников «Гильдии архивистов JC», под редакцией Александра Виноградова.",
                ImagePath = "Images/StarWarsEpizod7.png",
                QuantityInStorage = 3
            });
            db.Entities.Add(new Book
            {
                Name = "Звездные войны.Эпизод 2.",
                Genre = new Genre { GenreBook = "Фантастика" }.GenreBook,
                Author = "Роберт Сальваторе",
                Price = 250,
                Description = "Звёздные войны. Эпизод II: Атака клонов (англ. Star Wars Episode II:" +
                " Attack of the Clones) - роман Роберта Сальваторе, опубликованный издательством" +
                " Del Rey 23 апреля 2002 года.Книга является официальной адаптацией фильма" +
                " «Звёздные войны.Эпизод II: Атака клонов». В книгу вошло много расширенных сцен" +
                " из фильма, вырезанные, а так же эксклюзивные сцены.Издание в мягкой обложке " +
                "включало 32 страницы раскадровок битвы на Джеонозисе, вступиление от " +
                "Ричарда Мак - Каллума и несколько обзорных глав нового романа Мэтью Стовера" +
                " - «Уязвимая точка». Аудиоверсию романа читал Джонатан Девис.На русском языке " +
                "книга впервые вышла в 2002 году, в совместном издании московского издательства" +
                " «Эксмо - Пресс» и издательства «Terra Fantastica» из Санкт - Петербурга" +
                " в переводе Ян Юа.В феврале 2016 года издательство «Азбука» выпустило переиздание" +
                " книги, сохранив перевод Ян Юа, который был значительно переработан членом " +
                "«Гильдии архивистов» Валентином Матюшей, убравшим из романа выдуманные переводчиком" +
                " дополнения и исправивший прочие неточности и ошибки перевода.",
                ImagePath = "Images/StarWarsEpizod2.png",
                QuantityInStorage = 3
            });

            db.Entities.Add(new User { Email = "matroskin0711@gmail.com", Pass = "admin", IsAdmin = true });

            base.Seed(db);
        }
    }
}