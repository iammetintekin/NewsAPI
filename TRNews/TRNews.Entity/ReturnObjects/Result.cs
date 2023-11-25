using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.ReturnObjects
{
    public class Result
    {
        public readonly string Message;
        public readonly bool Success;
        public Result(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}
