namespace ZombieDAO.Controllers; 

[Route("api/users")]
public sealed class UsersController : ZombieDAOController {
    private readonly UserManager _userManager;

    public UsersController(UserManager userManager) {
        _userManager = userManager;
    }

    [HttpGet("whoami")]
    public ActionResult<UserDetailsDTO> WhoAmI() {
        return Ok(GetCurrentUser());
    }

    [HttpPost("start_registration"), AllowAll]
    public async Task<ActionResult<string>> StartRegistrations([FromBody] string wallet, CancellationToken token) {
        return Ok(await _userManager.StartRegistration(wallet, token));
    }

    [HttpPost("complete_registration"), AllowAll]
    public async Task<ActionResult<UserDetailsDTO>> CompleteRegistration([FromBody] CreateUserDTO dto, CancellationToken token) {
        var session = await _userManager.CompleteRegistration(dto, token);
        SetSessionCookie(session.ID);
        return Ok(session.User);
    }

    [HttpPost("start_login"), AllowAll]
    public async Task<ActionResult<string>> StartLogin([FromBody] string wallet, CancellationToken token) {
        return Ok(await _userManager.StartLogin(wallet, token));
    }

    [HttpPost("complete_login"), AllowAll]
    public async Task<ActionResult<UserDetailsDTO>> CompleteLogin([FromBody] CompleteLoginDTO dto, CancellationToken token) {
        var session = await _userManager.CompleteLogin(dto, token);
        SetSessionCookie(session.ID);
        return Ok(session.User);
    }

    [HttpGet("logout")]
    public ActionResult Logout() {
        _userManager.Logout(GetCurrentSessionID());
        ClearSessionCookie();
        return Ok();
    }
}
