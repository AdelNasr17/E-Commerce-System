using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce_System.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next ,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        //Response [StatusCode , ErrorMsg]

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next.Invoke(httpContext);
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandelNotFoundApiAsync(httpContext);
                }


            }
            catch (Exception ex)
            {
                //Loge Exception 
                _logger.LogError($"Something went wrong :{ex}");
                // Handle Exception 

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandelNotFoundApiAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The End Point {httpContext.Request.Path} Is Not Found "
            };
            await httpContext.Response.WriteAsync(response.ToString());


        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Set Content type[application/json]
            httpContext.Response.ContentType = "application/json";
            // set status Code  to 500
            //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;//500
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;//500


            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, //404
                _ => (int)HttpStatusCode.InternalServerError
            };


            // Response Object 
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };


            //Return object As Json
            await httpContext.Response.WriteAsync(response.ToString());
        }
    }
}
