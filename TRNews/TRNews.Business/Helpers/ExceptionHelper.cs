using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRNews.Business.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetExceptionMessage(this Exception exception)
        {
            return exception.ToString();
        }
    }
}
