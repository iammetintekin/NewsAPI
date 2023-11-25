using TRNews.Infrastructure.EntityFramework.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Infrastructure.Patterns.UnitOfWorkPattern
{
    public interface IUnitOfWorkService
    {
        public IReportingRepository Reportings { get; }
        public ICategoryRepository Categories { get; }
        int Save();
    }
}
