namespace ZombieDAO.Controllers; 

[Route("api/gnosis")]
public sealed class GnosisController : ZombieDAOController {
    private readonly GnosisManager _gnosisManager;

    public GnosisController(GnosisManager gnosisManager) {
        _gnosisManager = gnosisManager;
    }

    [HttpGet("{safeID:guid}")]
    public async Task<ActionResult<GnosisSafeDetailsDTO>> GetByID([FromRoute] Guid safeID, CancellationToken token) {
        return Ok(await _gnosisManager.GetByID(safeID, GetCurrentUser().Wallet, token));
    }

    [HttpPost("{projectID:guid}/create")]
    public async Task<ActionResult<GnosisSafeDetailsDTO>> Create([FromRoute] Guid projectID, CreateGnosisSafeDTO dto, CancellationToken token) {
        return Ok(await _gnosisManager.CreateGnosisSafe(projectID, dto, GetCurrentUser().Wallet, token));
    }

    [HttpPut("{safeID:guid}/add_token")]
    public async Task<ActionResult> AddToken([FromRoute] Guid safeID, [FromBody] CreateGnosisSafeTokenDTO dto, CancellationToken token) {
        await _gnosisManager.AddToken(safeID, dto, GetCurrentUser().Wallet, token);
        return Ok();
    }

    [HttpPut("{safeID:guid}/add_transaction")]
    public async Task<ActionResult> AddTransaction([FromRoute] Guid safeID, [FromBody] CreateGnosisSafeTransactionDTO dto, CancellationToken token) {
        await _gnosisManager.AddTransaction(safeID, dto, GetCurrentUser().Wallet, token);
        return Ok();
    }

    [HttpDelete("{safeID:guid}/delete_transaction/{id:guid}")]
    public async Task<ActionResult> DeleteTransaction([FromRoute] Guid safeID, [FromRoute] Guid id, CancellationToken token) {
        await _gnosisManager.DeleteTransaction(safeID, id, GetCurrentUser().Wallet, token);
        return Ok();
    }

    [HttpPut("{safeID:guid}/transactions/{txID:guid}/add_confirmation")]
    public async Task<ActionResult> AddConfirmation([FromRoute] Guid safeID, [FromRoute] Guid txID, [FromBody] CreateGnosisSafeConfirmationDTO dto, CancellationToken token) {
        await _gnosisManager.AddConfirmation(safeID, txID, dto, GetCurrentUser().Wallet, token);
        return Ok();
    }

    [HttpPut("${safeID:guid}/transactions/{txID:guid}/set_executed")]
    public async Task<ActionResult> SetTransactionExecuted([FromRoute] Guid safeID, [FromRoute] Guid txID, CancellationToken token) {
        await _gnosisManager.SetTransactionExecuted(safeID, txID, GetCurrentUser().Wallet, token);
        return Ok();
    }
}
