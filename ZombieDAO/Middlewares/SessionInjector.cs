namespace ZombieDAO.Middlewares; 

public sealed class SessionInjector {
    private readonly RequestDelegate _next;

    public SessionInjector(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context, UserManager userManager) {
        if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("api")) {
            var id = context.Request.Cookies["session-id"];
            if (id != null) {
                var user = userManager.CheckSessionID(Guid.Parse(id));
                if (user != null) context.Items["User"] = user;
            }
        }

        await _next(context);
    }
}
