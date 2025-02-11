using Newtonsoft.Json;
using System.Net;
using Fundraising_System.Application    .Exceptions;
using NotImplementedException = Fundraising_System.Application.Exceptions.NotImplementedException;
using UnauthorizedAccessException = Fundraising_System.Application.Exceptions.UnauthorizedAccessException;
using Microsoft.AspNetCore.Http;

namespace Fundraising_System.Application.Configurations
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            switch (exception)
            {
                case UnauthorizedAccessException unauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case NotImplementedException notImplementedException:
                    code = HttpStatusCode.NotImplemented;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                case System.Collections.Generic.KeyNotFoundException keyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { Error = exception.Message });
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
