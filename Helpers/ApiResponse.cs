using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EcommerceApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Helpers
{
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(HttpStatusCode statusCode, string message, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>(HttpStatusCode.OK, message, data);
        }

        public static ApiResponse<T> Created(T data, string message = "Created successfully")
        {
            return new ApiResponse<T>(HttpStatusCode.Created, message, data);
        }

        public static ApiResponse<T> Fail(HttpStatusCode statusCode, string message)
        {
            return new ApiResponse<T>(statusCode, message);
        }

        public static ApiResponse<T> BadRequest(string message = "Bad request")
        {
            return new ApiResponse<T>(HttpStatusCode.BadRequest, message);
        }

        public static ApiResponse<T> Unauthorized(string message = "Unauthorized")
        {
            return new ApiResponse<T>(HttpStatusCode.Unauthorized, message);
        }

        public static ApiResponse<T> NotFound(string message = "Not found")
        {
            return new ApiResponse<T>(HttpStatusCode.NotFound, message);
        }

        public static ApiResponse<T> ServerError(string message = "Internal server error")
        {
            return new ApiResponse<T>(HttpStatusCode.InternalServerError, message);
        }
    }
}