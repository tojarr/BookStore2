using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2.Models.Abstract
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }
        void Save(T entity);
        T GetById(object id);
        void Delete(T entity);
        void Edit(T entity);
    }
}
