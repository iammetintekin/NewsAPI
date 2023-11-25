using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity.Models;
using TRNews.Infrastructure.EntityFramework.Repository;

namespace TRNews.Infrastructure.EntityFramework.Repositories.Abstract
{
    public interface IReportingRepository : IRepositoryService<Reporting>
    {
    }
}
