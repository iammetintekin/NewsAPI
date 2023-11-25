using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.ReponseObjects
{
    public class ResponseDataObject<T>
    {
        public ResponseDataObject(T data, string message = "", int status = 200, List<string> errors = null)
        {
            Data = data;
            Message = message;
            Status = status;
            Errors = errors;
        }
        public ResponseDataObject(T data, PaginationOptions pagination, string message = "", int status = 200, List<string> errors = null)
        {
            Data = data;
            Message = message;
            Status = status;
            Errors = errors;
            Pagination = pagination;
        }
        public T Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public List<string> Errors { get; set; }
        public PaginationOptions Pagination { get; set; }
    }
    public class PaginationOptions
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
