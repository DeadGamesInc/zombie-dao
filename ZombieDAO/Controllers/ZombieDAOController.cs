using System.Diagnostics;

namespace ZombieDAO.Controllers; 

[ApiController, Authorize]
public class ZombieDAOController : ControllerBase {
    protected Guid GetCurrentSessionID() {
        var sessionID = Request.Cookies["session-id"];
        if (sessionID == null)
            throw new UnreachableException("Session ID not present in HTTP headers after session validation");
        return Guid.Parse(sessionID);
    }
    
    protected UserDetailsDTO GetCurrentUser() {
        var user = HttpContext.Items["User"];
        if (user == null) throw new UnreachableException("User not present in HTTP context after session validation");
        return (UserDetailsDTO) user;
    }
    
    protected void SetSessionCookie(Guid sessionID) {
        var options = new CookieOptions { HttpOnly = true };
        Response.Cookies.Append("session-id", sessionID.ToString(), options);
    }

    protected void ClearSessionCookie() {
        var options = new CookieOptions { HttpOnly = true, Expires = DateTime.UtcNow };
        Response.Cookies.Append("session-id", "", options);
    }
}
