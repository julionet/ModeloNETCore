using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Modelo.Interface
{
    public interface IRepository<T>
    {
        DbContext GetContext();
        string Insert(T entity);
        string Update(T entity);
        string Delete(T entity);
        string Delete(int id);
        void Cancel(T entity);
        string JoinEntity<TEntity>(ICollection<TEntity> list, string s);
        IQueryable<T> Filter(string condition);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetTop(int n);
        T GetById(int id);
        int GetCount();
        string SaveChanges();
    }
}
