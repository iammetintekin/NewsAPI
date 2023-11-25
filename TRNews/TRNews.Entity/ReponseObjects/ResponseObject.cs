using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.ReponseObjects
{
    public class ResponseObject
    {
        public ResponseObject(string message, int status, List<string> errors = null)
        {
            Message = message;
            Status = status;
            Errors = errors;
        }
        public string Message { get; set; }
        public int Status { get; set; }
        public List<string> Errors { get; set; }
    }
}
