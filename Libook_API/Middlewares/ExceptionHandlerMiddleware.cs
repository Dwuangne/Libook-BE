using System.Net;
using System.Text.Json;

namespace Libook_API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                // Ghi log chi tiết về lỗi
                var errorDetails = new
                {
                    ErrorId = errorId,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message,
                    Timestamp = DateTime.UtcNow,
                    RequestPath = httpContext.Request.Path,
                    RequestMethod = httpContext.Request.Method,
                    RequestHeaders = httpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                    UserAgent = httpContext.Request.Headers["User-Agent"].ToString(),
                    UserIpAddress = httpContext.Connection.RemoteIpAddress?.ToString()
                };

                // Ghi log lỗi với thông tin chi tiết
                logger.LogError(ex, $"Error {errorId}: {JsonSerializer.Serialize(errorDetails)}");

                // Trả về phản hồi lỗi tùy chỉnh
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Đã xảy ra lỗi! Chúng tôi đang xem xét để giải quyết vấn đề này."
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
