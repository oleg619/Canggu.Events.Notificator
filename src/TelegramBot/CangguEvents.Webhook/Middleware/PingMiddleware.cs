using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CangguEvents.Asp.Middleware
{
    public class PingMiddleware
    {
        private readonly RequestDelegate _next;

        public PingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals("/ping", StringComparison.CurrentCultureIgnoreCase))
            {
                await httpContext.Response.WriteAsync("pong");
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}