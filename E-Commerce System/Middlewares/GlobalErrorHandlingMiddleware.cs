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

                await _next(httpContext);


            }catch (Exception ex)
            {
                //Loge Exception 
                _logger.LogError($"Something went wrong :{ex}");
                // Handle Exception 

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
           //Set Content type[application/json]
           // set status Code  to 500
           // return standard response
           httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError;//500

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, //404
                _ => (int)HttpStatusCode.InternalServerError
            };
            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            }.ToString();
           
            await httpContext.Response.WriteAsync(response);
        }
    }
}
