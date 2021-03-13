using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ALBAB.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ALBAB.Errors
{
    public class ExceptionMiddleware // on Startup class only for internal server error 500 like null exceptions
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,IHostEnvironment env )
       //Check mode id developement or production
       // next: meaning what is coming up next in the midldleware pipeline
       // ILogger: for display what information we need to de dispalyed for us
        {
            _env = env;
            _logger = logger;
            _next = next;

        }

        public async Task InvokeAsync (HttpContext context) // adding middleware can access the context of Http request
        {
         try // we used try catch block of top one of middleware
         {
            await _next(context); // This is the top of middleware so any exception bellow it will through to up until reach the top one
         }   

          catch  (Exception ex )

          {
                _logger.LogError(ex, ex.Message); //console system, this original exception message
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError; // (500) interanal server error
                var response = _env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) //devolopement more details
                : new ApiException(context.Response.StatusCode, "Internal Server Error"); // production mode

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; // converting to camel case
               
                var json = JsonSerializer.Serialize(response,options); // response to camel case

                await context.Response.WriteAsync(json);


          }

        }



    }
}