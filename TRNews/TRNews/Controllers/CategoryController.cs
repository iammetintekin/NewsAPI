using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRNews.Business.LoadServices;
using TRNews.Entity;
using TRNews.Entity.Models;
using TRNews.Entity.ReponseObjects;
using TRNews.Utilities.Attributes;
using X.PagedList;

namespace TRNews.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IServiceManager _service; 
        public CategoryController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))] // checks if modelstate is valid
        [Authorize(Roles = "admin")]
        public IActionResult Create([FromQuery]string Name)
        {
            _service.Categories.Create(new Category
            {
                Id=0,
                Name = Name
            });
            return Ok(new ResponseObject("Kaydedildi.",200));
        }

        [Authorize]
        [HttpGet]
        [ModelValidationFilterAttribute] 
        public IActionResult List(string keyword="", int page=1)
        {
            var data = _service.Categories.List(new FilterModel(keyword,page:page));

            if (!data.Success)
                return BadRequest();

            var returnModel = new ResponseDataObject<IPagedList<Category>>(data.Data);

            returnModel.Pagination = new PaginationOptions
            {
                CurrentPage = returnModel.Data.PageNumber,
                TotalCount = returnModel.Data.Count,
                HasNextPage = returnModel.Data.HasNextPage,
                HasPreviousPage = returnModel.Data.HasPreviousPage
            };

            return Ok(returnModel);
        }
    }
}
