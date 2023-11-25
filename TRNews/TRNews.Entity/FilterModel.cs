using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity
{
    public class FilterModel
    {
        public string keyword { get; init; } 
        public int page { get; init; } 
        public DateTime? date { get; init; }
        public int? PublishedUserId { get; set; }
        public bool? Active { get; set; }
        public int? CategoryId { get; set; }
        public FilterModel(string keyword = "", DateTime? date=null, int page=1, int? publishedUserId=null, bool? active=null, int? categoryid=null)
        {
            this.keyword = keyword;
            this.date = date;
            this.page = page;
            PublishedUserId = publishedUserId;
            Active = active;
            CategoryId = categoryid;
        }
    }
}
