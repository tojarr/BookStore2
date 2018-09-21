using BookStore2.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookStore2.Models
{
    //public class BookContext : DbContext
    //{
    //    public DbSet<Book> Books { get; set; }
    //    public DbSet<User> Users { get; set; }


    //}

    public class EFDbContext : DbContext
    {
        public DbSet<BaseEntity> Entities { get; set; }

        public EFDbContext()
           : base("name=DbConnectionString")
        {
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }

    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly EFDbContext _context;
        private IDbSet<T> entities;
        string errorMessage = string.Empty;

        public EFRepository(EFDbContext context)
        {
            _context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        //public T GetByObject (T entity)
        //{
        //    entity = Table.ToList().FirstOrDefault(u => u.)
        //    return entity;
        //}

        public void Save(T entity)
        {
            this.Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Edit(T entity)
        {
            //db.Entry(book).State = EntityState.Modified;
            //db.SaveChanges();
            _context.SaveChanges();
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        public void Delete(T entity)
        {
            this.Entities.Remove(entity);
            _context.SaveChanges();
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = _context.Set<T>();
                }
                return entities;
            }
        }
    }
}