namespace ZombieDAO.Controllers; 

[Route("api")]
public sealed class GeneralController : ZombieDAOController {
    [HttpGet("app_setup"), AllowAll]
    public ActionResult<AppSetupDTO> GetAppSetup() {
        var dto = new AppSetupDTO { SupportedBlockchains = Globals.SupportedBlockchains };
        return Ok(dto);
    }
}
