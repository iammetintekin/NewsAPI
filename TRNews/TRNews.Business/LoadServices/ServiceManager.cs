using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Business.Services.Abstract;
using TRNews.Business.Services.Concrete;
using TRNews.Entity.Models;
using TRNews.Infrastructure.Patterns.UnitOfWorkPattern;

namespace TRNews.Business.LoadServices
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IUserService> _userService;

        private readonly Lazy<IReportingService> _reportingService; 

        private readonly Lazy<ICategoryService> _categoryService;

        private readonly Lazy<ILoggerService> _logger;

        public ServiceManager(IUnitOfWorkService db, IMapper mapper, SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userService = new Lazy<IUserService>(()=>new UserService(userManager, signInManager, mapper, config, httpContextAccessor));
            _reportingService = new Lazy<IReportingService>(()=>new ReportingService(db, mapper, userManager));
            _categoryService = new Lazy<ICategoryService>(()=>new CategoryService(db));
            _logger = new Lazy<ILoggerService>(()=>new LoggerService());
        }

        public IUserService Users => _userService.Value;

        public IReportingService Reportings => _reportingService.Value;

        public ICategoryService Categories => _categoryService.Value;

        public ILoggerService Logger => _logger.Value;
    }
}
