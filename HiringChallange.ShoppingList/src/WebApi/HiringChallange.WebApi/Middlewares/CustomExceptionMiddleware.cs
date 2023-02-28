using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace HiringChallange.WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {

        private static readonly Serilog.ILogger _logger = Log.ForContext<CustomExceptionMiddleware>();
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                string message = "[Request] HTTP " + httpContext.Request.Method + " " + httpContext.Request.Path;
                _logger.Error(message);

                await _next(httpContext);

                message = "[Request] HTTP " + httpContext.Request.Method + " " + " reponded " + httpContext.Response.StatusCode;
                _logger.Error(message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);

            }
        }


        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[ERROR] HTTP " + httpContext.Request.Method + " - " + httpContext.Response.StatusCode + " - " + " Error Messsage " + ex.Message;

            _logger.Error(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return httpContext.Response.WriteAsync(result);

        }

    }
}
