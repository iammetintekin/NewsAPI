using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity.Models;

namespace TRNews.Entity.DTOs
{
    public class ReportingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string WriterName { get; set; }
        public string CategoryName { get; set; }
    }
}
