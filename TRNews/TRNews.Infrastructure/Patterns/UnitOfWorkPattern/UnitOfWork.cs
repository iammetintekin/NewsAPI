using TRNews.Infrastructure.EntityFramework;
using TRNews.Infrastructure.EntityFramework.Repositories.Abstract;
using TRNews.Infrastructure.EntityFramework.Repositories.Concrete;
using System;
 

namespace TRNews.Infrastructure.Patterns.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWorkService
    {
        private DatabaseContext _databaseContext;
        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        private ReportingRepository _reportings;
        public IReportingRepository Reportings => _reportings ?? new ReportingRepository(_databaseContext);

        private CategoryRepository _histories;
        public ICategoryRepository Categories => _histories ?? new CategoryRepository(_databaseContext);

        public int Save()
        {
            return _databaseContext.SaveChanges();
        }
    }
}
