using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ScienceFileUploader.Entities;
using ScienceFileUploader.Exceptions.Shared;

namespace ScienceFileUploader.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    BadRequestException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
        }
    }
}