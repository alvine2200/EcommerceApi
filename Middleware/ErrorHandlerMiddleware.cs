using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Helpers;
using System.Net;
using System.Text.Json;
using EcommerceApi.Exceptions;

namespace EcommerceApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            ApiResponse<object> response;

            switch (ex)
            {
                case NotFoundException nf:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = ApiResponse<object>.Fail(HttpStatusCode.NotFound, nf.Message);
                    break;

                case UnauthorizedException un:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = ApiResponse<object>.Fail(HttpStatusCode.Unauthorized, un.Message);
                    break;

                case ValidationException ve:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse<object>(
                        HttpStatusCode.BadRequest,
                        ve.Message,
                        ve.Errors
                    );
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<object>.ServerError(ex.Message);
                    break;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}