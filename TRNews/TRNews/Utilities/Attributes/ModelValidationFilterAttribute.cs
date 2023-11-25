using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using TRNews.Entity.ReponseObjects;
using Microsoft.AspNetCore.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TRNews.Utilities.Attributes
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                var errors = new List<string>();
                foreach (var modelState in context.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                var errorModel = new ResponseObject("Invalid Object. ", StatusCodes.Status400BadRequest, errors);
                context.Result = new BadRequestObjectResult(errorModel);
            }

            if (context.HttpContext.User == null)
            {
                var errorModel = new ResponseObject("Auth failed. ", StatusCodes.Status400BadRequest, new List<string> { "Lütfen giriş yapınız." });
                context.Result = new BadRequestObjectResult(errorModel);
            }
        }
    }
}
