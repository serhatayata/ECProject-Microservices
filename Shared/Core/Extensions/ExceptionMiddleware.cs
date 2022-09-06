using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Core.CrossCuttingConcerns.Logging.ElasticSearch;
using Microsoft.Extensions.Configuration;
using Core.CrossCuttingConcerns.Logging;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private IElasticSearchService _client;
        private readonly IConfiguration _configuration;

        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration, IElasticSearchService client)
        {
            _next = next;
            _client = client;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.Unauthorized);
            }
            catch (BadHttpRequestException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.BadRequest);
            }
            catch (ValidationException e)
            {
                await HandleExceptionAsync(httpContext, e, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            object message = "Internal Server Error";

            var defaultIndex = _configuration["ELKConfiguration:index"];

            //Şayet oluşan hata Bizim belirlediğimiz validasyon kontrolünden geliyorsa oradan gelen hatayı kullanıcıya dönder.

            if (e.GetType() == typeof(ValidationException))
                message = (e as ValidationException)?.Errors.Select(s => s.ErrorMessage);
            //else message=e.Message kısmı silinecek. Sadece görmek için eklendi.
            else
                message = e.Message;

            var logDetail = new LogDetail
            {
                MethodName = "Exception",
                Explanation=message.ToString(),
                LogParameters = null,
                Risk = 1,
                LoggingTime = DateTime.Now.ToString()
            };

            await _client.Add(logDetail);

            //return httpContext.Response.WriteAsJsonAsync(new ErrorDetails
            //{
            //    StatusCode = httpContext.Response.StatusCode,
            //    Message = message,
            //    Success = false
            //});
        }
    }
}
