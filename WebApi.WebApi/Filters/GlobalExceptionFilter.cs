using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using WebApi.Domain.Exceptions;

namespace WebApi.WebApi.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if(context.Exception.GetType() == typeof(BadRequestException))
            {
                var exception = (BadRequestException)context.Exception;
                var validation = new
                {
                    Status = HttpStatusCode.BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                /*var json = new
                {
                    errors = new[] { validation }
                };*/

                context.Result = new BadRequestObjectResult(validation);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
            else if(context.Exception.GetType() == typeof(NotFoundException))
            {
                var exception = (NotFoundException)context.Exception;
                var validation = new
                {
                    Status = HttpStatusCode.NotFound,
                    Title = "Not found exception",
                    Detail = exception.Message
                };

                /*var json = new
                {
                    errors = new[] { validation }
                };*/

                context.Result = new ObjectResult(validation);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.ExceptionHandled = true;
            }
            else if(context.Exception.GetType() == typeof(InternalServerException))
            {
                var exception = (InternalServerException)context.Exception;
                var validation = new
                {
                    Status = HttpStatusCode.InternalServerError,
                    Title = "Internal server error",
                    Detail = exception.Message
                };

                /*var json = new
                {
                    errors = new[] { validation }
                };*/

                context.Result = new ObjectResult(validation);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }else if (context.Exception.GetType() == typeof(ApiException))
            {
                var exception = (ApiException)context.Exception;
                var validation = new
                {
                    Status = exception.StatusCode,
                    Title = "Error",
                    Detail = exception.Message
                };

                /*var json = new
                {
                    errors = new[] { validation }
                };*/

                context.Result = new ObjectResult(validation);
                context.HttpContext.Response.StatusCode = (int)exception.StatusCode;
                context.ExceptionHandled = true;
            }
            else
            {
                _logger.LogError(context.Exception.ToString());
                var validation = new
                {
                    Status = HttpStatusCode.InternalServerError,
                    Title = "Internal server error",
                    Detail = "Error en el servidor"
                };

                /*var json = new
                {
                    errors = new[] { validation }
                };*/

                context.Result = new ObjectResult(validation);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
            }
        }
    }
}