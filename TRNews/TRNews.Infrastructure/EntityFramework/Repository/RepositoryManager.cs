using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace TRNews.Infrastructure.EntityFramework.Repository
{
    public class RepositoryManager<T> : IRepositoryService<T> where T : class
    {
        private DbSet<T> _dbSet;
        public RepositoryManager(DbContext db)
        {
            _dbSet = db.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).SingleOrDefault();
        }

        public IQueryable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _dbSet : _dbSet.Where(filter);
        }

        public IPagedList<T> PagedList(System.Linq.Expressions.Expression<Func<T, bool>> filter, int page, int take)
        {
            return filter == null ? _dbSet.ToPagedList(page, take) : _dbSet.Where(filter).ToPagedList(page, take);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
