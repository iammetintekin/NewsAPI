using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRNews.Business.LoadServices;
using TRNews.Entity.Models;
using TRNews.Entity.ReponseObjects;
using TRNews.Entity;
using TRNews.Utilities.Attributes;
using X.PagedList;
using TRNews.Entity.DTOs;
using AutoMapper;

namespace TRNews.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportingController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;
        public ReportingController(IServiceManager service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))] // checks if modelstate is valid
        [Authorize]
        public async Task<IActionResult> Create(ReportingCreateDto model)
        {
            if (!_service.Categories.Exist(model.CategoryId))
                return BadRequest(new ResponseObject("Hatalı kategori",StatusCodes.Status400BadRequest));

            var mappedData = _mapper.Map<Reporting>(model);
            var currentUser = await _service.Users.CurrentUser();

            mappedData.UserId = currentUser.Id;

            var createResult = _service.Reportings.Create(mappedData);

            if(!createResult.Success)
                return BadRequest(new ResponseObject(createResult.Message, StatusCodes.Status400BadRequest));

            return Ok(new ResponseObject(createResult.Message, StatusCodes.Status200OK));
        }

        // login yapan kullanıcıların görebildiği, yazar bazlı filtreleme de yapan endpoint.
        [Authorize]
        [HttpGet]
        public IActionResult List(string keyword = "", int page = 1, DateTime? date=null, int? publishedUser = null, bool? active=null, int? categoryid=null)
        {
            var data = _service.Reportings.List(new FilterModel(keyword, date, page: page,publishedUser,active, categoryid));

            if (!data.Success)
                return BadRequest();

            var returnModel = new ResponseDataObject<IPagedList<ReportingDto>>(data.Data);
            returnModel.Pagination = new PaginationOptions
            {
                CurrentPage = returnModel.Data.PageNumber,
                TotalCount = returnModel.Data.Count,
                HasNextPage = returnModel.Data.HasNextPage,
                HasPreviousPage = returnModel.Data.HasPreviousPage
            };
            return Ok(returnModel);
        }

        // example url : /api/Reporting/Read/sosyal-medya-fenomeni-resit-bozdag-tutuklandi
        [Authorize]
        [HttpGet]
        [Route("{slug}")]
        public IActionResult Read(string slug)
        {
            var data = _service.Reportings.GetBySlug(slug); 
            if (!data.Success)
                return BadRequest();
            var returnModel = new ResponseDataObject<ReportingDto>(data.Data);
            return Ok(returnModel);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var data = _service.Reportings.GetById(id);
            if (!data.Success)
                return BadRequest();
            var returnModel = new ResponseDataObject<ReportingDto>(data.Data);
            return Ok(returnModel);
        }

        [HttpPut]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [Authorize]
        public async Task<IActionResult> Update(ReportingUpdateDto model)
        {
            if (!_service.Categories.Exist(model.CategoryId))
                return BadRequest(new ResponseObject("Hatalı kategori seçildi", StatusCodes.Status400BadRequest));

            var mappedData = _mapper.Map<Reporting>(model);
            mappedData.Updated = DateTime.Now;

            var currentUser = await _service.Users.CurrentUser();
            if(!_service.Reportings.IsPublisher(mappedData.Id, currentUser.Id))
                return BadRequest(new ResponseObject("Bu haber size ait değil.", StatusCodes.Status400BadRequest));

            var updateResult = _service.Reportings.Update(mappedData);
            if (!updateResult.Success)
                return BadRequest(new ResponseObject(updateResult.Message, StatusCodes.Status400BadRequest));

            return Ok(new ResponseObject(updateResult.Message, StatusCodes.Status200OK));
        }
        [HttpDelete]
        [ServiceFilter(typeof(ModelValidationFilterAttribute))]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_service.Reportings.Exist(id))
                return BadRequest(new ResponseObject("Hatalı id gönderildi.", StatusCodes.Status400BadRequest));
 
            var currentUser = await _service.Users.CurrentUser();
            if (!_service.Reportings.IsPublisher(id, currentUser.Id))
                return BadRequest(new ResponseObject("Bu haberi silmek için yetkili değilsiniz.", StatusCodes.Status400BadRequest));

            var deleteResult = _service.Reportings.DeleteById(id);
            if (!deleteResult.Success)
                return BadRequest(new ResponseObject(deleteResult.Message, StatusCodes.Status400BadRequest));

            return Ok(new ResponseObject(deleteResult.Message, StatusCodes.Status200OK));
        }
    }
}
