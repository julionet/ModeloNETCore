using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Modelo.Interface;
using Modelo.Entity;

namespace Modelo.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseClass
    {
        private DbContext _dbcontext;

        private DbSet<T> DbSet
        {
            get { return _dbcontext.Set<T>(); }
        }

        public Repository(DbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public DbContext GetContext()
        {
            return _dbcontext;
        }

        public string Insert(T entity)
        {
            DbSet.Add(entity);
            return this.SaveChanges();
        }

        public string Update(T entity)
        {
            DbSet.Attach(entity);
            _dbcontext.Entry<T>(entity).State = EntityState.Modified;
            return this.SaveChanges();
        }

        public string Delete(T entity)
        {
            DbSet.Remove(entity);
            return this.SaveChanges();
        }

        public string Delete(int id)
        {
            T entity = GetById(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                return this.SaveChanges();
            }
            else
                return "";
        }

        public void Cancel(T entity)
        {
            //dbContext.Entry<T>(entity).Reload();
        }

        public string JoinEntity<TEntity>(ICollection<TEntity> list, string s)
        {
            if (list != null)
            {
                while (list.Count() != 0)
                    list.Remove(list.ToArray()[0]);

                if (!string.IsNullOrWhiteSpace(s))
                {
                    foreach (string _item in s.Split('|'))
                        if (_item != "")
                        {
                            //TEntity item = (TEntity)_dbcontext.Set<TEntity>().SingleOrDefault(p => p.Id == Convert.ToInt32(_item));
                            //if (item != null) list.Add(item);
                        }
                }

                return this.SaveChanges();
            }
            else
                return "";
        }

        public IQueryable<T> Filter(string condition)
        {
            return DbSet.Where(condition);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public IQueryable<T> GetTop(int n)
        {
            return DbSet.Take(n);
        }

        public T GetById(int id)
        {
            return DbSet.SingleOrDefault(p => p.Id == id);
        }

        public int GetCount()
        {
            return DbSet.Count();
        }

        public string DeleteRange(List<T> entities)
        {
            DbSet.RemoveRange(entities);
            return this.SaveChanges();
        }

        public string SaveChanges()
        {
            try
            {
                _dbcontext.SaveChanges();
                return "";
            }
            catch (Exception erro)
            {
                if (erro.InnerException != null)
                {
                    if (erro.InnerException.InnerException != null)
                        return erro.InnerException.InnerException.Message;
                    else
                        return erro.InnerException.Message;
                }
                else
                    return erro.Message;
            }
        }
    }
}
