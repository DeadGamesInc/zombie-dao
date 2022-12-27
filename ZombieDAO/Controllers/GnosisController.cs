namespace ZombieDAO.Controllers; 

[Route("api/gnosis")]
public sealed class GnosisController : ZombieDAOController {
    private readonly GnosisManager _gnosisManager;

    public GnosisController(GnosisManager gnosisManager) {
        _gnosisManager = gnosisManager;
    }

    [HttpGet("{safeID:guid}")]
    public async Task<ActionResult<GnosisSafeDetailsDTO>> GetByID([FromRoute] Guid safeID, CancellationToken token) {
        return Ok(await _gnosisManager.GetByID(safeID, token));
    }

    [HttpPost("{projectID:guid}/create")]
    public async Task<ActionResult<GnosisSafeDetailsDTO>> Create([FromRoute] Guid projectID, CreateGnosisSafeDTO dto, CancellationToken token) {
        return Ok(await _gnosisManager.CreateGnosisSafe(projectID, dto, GetCurrentUser().Wallet, token));
    }
}
