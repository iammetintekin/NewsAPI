using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TRNews.Business.Helpers;
using TRNews.Business.Services.Abstract;
using TRNews.Entity;
using TRNews.Entity.DTOs;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;
using TRNews.Infrastructure.Patterns.UnitOfWorkPattern;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TRNews.Business.Services.Concrete
{
    public class ReportingService : IReportingService
    {
        private readonly IUnitOfWorkService _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ReportingService(IUnitOfWorkService db, IMapper mapper, UserManager<User> userManager)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
        }
        public Result Create(Reporting entity)
        {
            try
            {
                entity.Created = DateTime.Now;
                entity.Updated = DateTime.Now;
                entity.Slug = ToUrlSlug(entity.Title);

                _db.Reportings.Create(entity);
                _db.Save();
            }
            catch (Exception ex)
            {
                return new Result($"Error: {ex.GetExceptionMessage()} ", false);
            }

            return new Result($"Başarılı.", true);
        }
        public DataResult<IPagedList<ReportingDto>> List(FilterModel filter)
        {
            var predicate = PredicateBuilder.New<Reporting>();

            if (!string.IsNullOrEmpty(filter.keyword))
                predicate.And(s => s.Title.Contains(filter.keyword) || s.Description.Contains(filter.keyword));

            if (filter.date is not null)
                predicate.And(s => s.Created >= filter.date);

            if (filter.PublishedUserId is not null)
                predicate.And(s => s.UserId == filter.PublishedUserId);

            if (filter.Active is not null)
                predicate.And(s => s.Active == filter.Active);

            if (filter.CategoryId is not null)
                predicate.And(s => s.CategoryId == filter.CategoryId);

            if (filter.date is null && string.IsNullOrEmpty(filter.keyword) && filter.PublishedUserId == null && filter.Active == null && filter.CategoryId is null)
                predicate = null;

            var data = _db.Reportings.List(predicate).ToList();

            foreach (var item in data)
            {
                item.Category = _db.Categories.Get(s => s.Id == item.Id);
                item.IdentityUser = _userManager.FindByIdAsync(item.UserId.ToString()).Result;
            }

            var mappedData = _mapper.Map<List<ReportingDto>>(data);

            var pagedData = mappedData.ToPagedList(filter.page, 10);

            return new DataResult<IPagedList<ReportingDto>>(pagedData, "", true);
        }
        public DataResult<ReportingDto> GetById(int Id)
        {
            var anyReport = _db.Reportings.Any(s => s.Id.Equals(Id));
            if (anyReport)
            {
                var data = _db.Reportings.Get(s => s.Id.Equals(Id));
                data.Category = _db.Categories.Get(s => s.Id == data.CategoryId);
                data.IdentityUser = _userManager.FindByIdAsync(data.UserId.ToString()).Result;

                // user gelecek
                return new DataResult<ReportingDto>(_mapper.Map<ReportingDto>(data), "Haber detayı.", true);
            }
            return new DataResult<ReportingDto>(null, $"Haber bulunamadı. Hatalı Id : {Id}", false);
        }
        public DataResult<ReportingDto> GetBySlug(string slug)
        {
            var anyReport = _db.Reportings.Any(s => s.Slug.Equals(slug));
            if (anyReport)
            {
                var data = _db.Reportings.Get(s => s.Slug.Equals(slug));
                data.Category = _db.Categories.Get(s => s.Id == data.CategoryId);
                data.IdentityUser = _userManager.FindByIdAsync(data.UserId.ToString()).Result;
                return new DataResult<ReportingDto>(_mapper.Map<ReportingDto>(data), "Haber detayı.", true);
            }
            return new DataResult<ReportingDto>(null, $"Haber bulunamadı. Hatalı adres : {slug}", false);
        }
        public bool Exist(int Id)
        {
            return _db.Reportings.Any(s => s.Id == Id);
        }

        public Result Update(Reporting data)
        {
            try
            {
                data.Slug = ToUrlSlug(data.Title);
                _db.Reportings.Update(data);
                _db.Save();
            }
            catch (Exception ex)
            {
                return new Result($"Error: {ex.GetExceptionMessage()} ", false);
            }
            return new Result($"Başarılı.", true);
        }
        public Result DeleteById(int Id)
        {
            var data = _db.Reportings.Get(s => s.Id.Equals(Id));
            _db.Reportings.Delete(data);
            _db.Save();
            return new Result($"Haber silindi. ", true);
        }
        private static string ToUrlSlug(string value)
        {
            value = value.ToLowerInvariant();
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(value);
            value = Encoding.ASCII.GetString(bytes);
            value = Regex.Replace(value, @"\s", "-", RegexOptions.Compiled);
            value = Regex.Replace(value, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
            value = value.Trim('-', '_');
            value = Regex.Replace(value, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            return value;
        }
        public bool IsPublisher(int ReportId, int UserID)
        {
            var result = _db.Reportings.Any(s => s.Id == ReportId && s.UserId == UserID);
            return result;
        }
    }
}
