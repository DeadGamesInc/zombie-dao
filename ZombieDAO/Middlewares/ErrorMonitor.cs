using System.Net;

namespace ZombieDAO.Middlewares; 

public sealed class ErrorMonitor {
    private readonly Serilog.ILogger _logger = Log.Logger.ForContext<ErrorMonitor>();
    private readonly RequestDelegate _next;

    public ErrorMonitor(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
        try {
            await _next(context);
        }
        catch (Exception error) {
            var response = context.Response;
            response.ContentType = "application/json";

            string? message = null;

            switch (error) {
                case InternalException exception:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    message = exception.SendMessage;
                    break;
                
                case NotAllowedException exception:
                    response.StatusCode = (int) HttpStatusCode.Forbidden;
                    message = exception.SendMessage;
                    break;
                
                case NotFoundException exception:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    message = exception.SendMessage;
                    break;
                
                case ConflictException exception:
                    response.StatusCode = (int) HttpStatusCode.Conflict;
                    message = exception.SendMessage;
                    break;
                
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    _logger.Warning(error, "Unusual exception caught at error middleware");
                    break;
            }

            if (message != null) await response.WriteAsync(message);
        }
    }
}
