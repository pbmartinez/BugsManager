using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApi.Filters
{
    public class ValidationProblemDetailsFilter : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            // Do something before the action executes.
            
            // next() calls the action method.
            var resultContext = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
            if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest
                || context.HttpContext.Response.StatusCode == StatusCodes.Status422UnprocessableEntity)
            {
                var validation = new ValidationProblemDetails(context.ModelState);
                var jsonRaw = Newtonsoft.Json.JsonConvert.SerializeObject(validation);

                MemoryStream responseBody = new MemoryStream();
                responseBody.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(responseBody))
                {
                    var actionResult = sr.ReadToEnd();
                    
                    Console.WriteLine(actionResult);
                }
                await resultContext.HttpContext.Response.WriteAsync(jsonRaw);
                await resultContext.HttpContext.Response.Body.FlushAsync();
                 
            }
        }
    }
}
