using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Business.Services.Abstract;

namespace TRNews.Business.LoadServices
{
    public interface IServiceManager
    {
        public IUserService Users { get; }
        public IReportingService Reportings { get; }
        public ICategoryService Categories { get; }
        public ILoggerService Logger { get; }
    }
}
