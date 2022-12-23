using Microsoft.AspNetCore.Mvc.Filters;

namespace ZombieDAO.Attributes; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter {
    public void OnAuthorization(AuthorizationFilterContext context) {
        var skip = context.ActionDescriptor.EndpointMetadata.OfType<AllowAllAttribute>().Any();
        if (skip) return;

        var user = (UserDetailsDTO?) context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}
