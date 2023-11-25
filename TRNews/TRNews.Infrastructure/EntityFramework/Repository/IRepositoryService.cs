using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace TRNews.Infrastructure.EntityFramework.Repository
{
    public interface IRepositoryService<T>
    {
        IQueryable<T> List(Expression<Func<T, bool>> filter = null);
        IPagedList<T> PagedList(Expression<Func<T, bool>> filter, int page, int take);
        T Get(Expression<Func<T, bool>> filter);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        bool Any(Expression<Func<T, bool>> filter);
    }
}
