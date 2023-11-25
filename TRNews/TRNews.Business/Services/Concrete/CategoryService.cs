using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Business.Helpers;
using TRNews.Business.Services.Abstract;
using TRNews.Entity;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;
using TRNews.Infrastructure.Patterns.UnitOfWorkPattern;
using X.PagedList;

namespace TRNews.Business.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWorkService _db;

        public CategoryService(IUnitOfWorkService db)
        {
            _db = db;
        }

        public void Create(Category entity)
        {
            _db.Categories.Create(entity);
            _db.Save();
        }

        public bool Exist(int Id)
        {
            return _db.Categories.Any(s=>s.Id == Id);
        }

        public DataResult<IPagedList<Category>> List(FilterModel filter)
        {
            var predicate = PredicateBuilder.New<Category>();

            if (!string.IsNullOrEmpty(filter.keyword))
                predicate.And(s => s.Name.Contains(filter.keyword));
            else
                predicate = null;

            var data = _db.Categories.PagedList(predicate, filter.page, 10);

            return new DataResult<IPagedList<Category>>(data, "", true);
        }
    }
}
