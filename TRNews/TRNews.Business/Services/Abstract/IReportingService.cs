using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity;
using TRNews.Entity.DTOs;
using TRNews.Entity.Models;
using TRNews.Entity.ReturnObjects;
using X.PagedList;

namespace TRNews.Business.Services.Abstract
{
    public interface IReportingService
    {
        bool IsPublisher(int ReportId, int UserID);
        Result Create(Reporting entity);
        DataResult<IPagedList<ReportingDto>> List(FilterModel filter);
        DataResult<ReportingDto> GetById(int Id);
        DataResult<ReportingDto> GetBySlug(string slug);
        Result Update(Reporting data);
        Result DeleteById(int Id);
        bool Exist(int Id);

    }
}
