using Newtonsoft.Json;
using Ordering.Application.Exceptions;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using NotFoundException = Ordering.Application.Exceptions.NotFoundException;

namespace Ordering.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext) {

            try {

                 await _next(httpContext);
            }
            catch(Exception ex) {

                await HandleExceptionAsync(httpContext,ex);
            }
        
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            string result = JsonConvert.SerializeObject(new ErrorDetails { ErrorMessage = ex.Message, ErrorType = "Failure" });

            switch (ex)
            {

   
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                default:
                    break;

            }

            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);

        }
    }
}


class ErrorDetails {

    public string ErrorType { get; set; }
    public string ErrorMessage { get; set; }
}