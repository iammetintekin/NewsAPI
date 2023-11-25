using AutoMapper;
using System.Collections.Generic;
using TRNews.Entity.DTOs;
using TRNews.Entity.DTOs.Identity;
using TRNews.Entity.Models;
using X.PagedList;

namespace TRNews.Utilities
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<ReportingCreateDto, Reporting>();
            CreateMap<ReportingUpdateDto, Reporting>();
            CreateMap<ReportingDto, Reporting>().ReverseMap()
                .ForMember(s=>s.WriterName, k=>k.MapFrom(t=>t.IdentityUser.UserName))
                .ForMember(s=>s.CategoryName, k=>k.MapFrom(t=>t.Category.Name));
        }
    }
}
