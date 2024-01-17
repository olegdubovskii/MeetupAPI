using MeetupAPI.Core.Exceptions;
using MeetupAPI.Core.Exceptions.NotFound;
using MeetupAPI.Core.Exceptions.Validation;
using System.Net;

namespace MeetupAPI.PresentationLayer.Middlewares.ExceptionMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            (int, string) errorInfo = exception switch
            {
                NotFoundException => ((int)HttpStatusCode.NotFound, exception.Message),
                UserSignUpException => ((int)HttpStatusCode.BadRequest, exception.Message),
                RefreshTokenMissingException => ((int)HttpStatusCode.Unauthorized, exception.Message),
                SignInWasFailedException => ((int)HttpStatusCode.Unauthorized, exception.Message),
                InvalidTokenException => ((int)HttpStatusCode.BadRequest, exception.Message),
                ValidationException => ((int)HttpStatusCode.BadRequest, exception.Message),
                _ => ((int)HttpStatusCode.InternalServerError, exception.Message)
            };

            context.Response.StatusCode = errorInfo.Item1;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = errorInfo.Item1,
                Message = errorInfo.Item2
            }.ToString());
        }
    }
}
