using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Entity.ReturnObjects
{
    public class DataResult<T>
    {
        public readonly T Data;
        public readonly string Message;
        public readonly bool Success;
        public DataResult(T data, string message, bool success)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }
}
