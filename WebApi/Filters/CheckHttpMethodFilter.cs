using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Internal;
using System;
using System.Linq;

namespace WebApi.Filters
{
    public class CheckHttpMethodFilter : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var method = context.HttpContext.Request.Method;
            var action = context.ActionDescriptor;
            var actionMethodContraint = action.ActionConstraints.FirstOrDefault(a => a.GetType() == typeof(HttpMethodActionConstraint));
            if (actionMethodContraint != null && !((HttpMethodActionConstraint)actionMethodContraint).HttpMethods.Contains(method))
                context.Result = new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //
        }
    }
}
