using CleanArchitecture.Common.ApiResponse;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistence.DbContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitecture.Common.Enums.ResponseEnums;

namespace CleanArchitecture.Application.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CleanArchitectureDbContext _context;

        public ExceptionMiddleware(RequestDelegate next, CleanArchitectureDbContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Error error = new Error();
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                error.UserId = httpContext.User.Identity.Name;
                error.StatusCode = httpContext.Response.StatusCode;
                await _context.AddAsync(error);
                await _context.SaveChangesAsync();
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ApiResponse()
            {
                Status = (int)Number.Zero,
                Message = ResponseMessage.Error,
                StatusCode = context.Response.StatusCode
            }.ToString());
        }
    }
}
