using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using rbac_core.Errors;

namespace rbac_core.Middleware
{
    public class HttpMiddleware(ILogger<HttpMiddleware> logger, RequestDelegate next)
    {
        private readonly ILogger<HttpMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var statusCode = ex switch
                {
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                    ValidationException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    AlreadyExistsException => StatusCodes.Status409Conflict,
                    NotAllowedException => StatusCodes.Status403Forbidden,
                    _ => StatusCodes.Status500InternalServerError,
                };

                var logMessage = GenerateLogMessage(httpContext, ex);
                _logger.LogError(logMessage);

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = statusCode;

                var errorResponse = new
                {
                    Success = false,
                    Message = statusCode == StatusCodes.Status500InternalServerError
                        ? "An Unexpected Server Error Occurred"
                        : ex.Message,
                    Error = ex.Message,
                };

                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }

        private static string GenerateLogMessage(HttpContext context, Exception ex)
        {
            var request = context.Request;

            var sb = new StringBuilder();
            sb.AppendLine("===== ERROR LOG =====");
            sb.AppendLine($"Timestamp: {DateTime.UtcNow:O}");
            sb.AppendLine($"Method: {request.Method}");
            sb.AppendLine($"Path: {request.Path}");
            sb.AppendLine($"Query: {request.QueryString}");
            sb.AppendLine($"Client IP: {context.Connection.RemoteIpAddress}");
            sb.AppendLine($"User Agent: {request.Headers.UserAgent}");
            sb.AppendLine(
                $"Status Code: {(ex is AppException ? "Handled App Exception" : "Unhandled Exception")}"
            );
            sb.AppendLine($"Error Type: {ex.GetType().Name}");
            sb.AppendLine($"Error Message: {ex.Message}");
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(ex.StackTrace);
            sb.AppendLine("====================");

            string logMessage = sb.ToString();
            return logMessage;
        }
    }
}
