using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;
using X.PagedList;

namespace TRNews.Business.Services.Abstract
{
    public interface ICategoryService
    {
        void Create(Category entity);
        bool Exist(int Id);
        DataResult<IPagedList<Category>> List(FilterModel filter);
    }
}
