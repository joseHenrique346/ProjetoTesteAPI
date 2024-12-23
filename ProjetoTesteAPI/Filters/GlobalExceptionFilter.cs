using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProjetoTesteAPI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new JsonResult(new { message = exception.Message })
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
