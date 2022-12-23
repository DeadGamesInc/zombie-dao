namespace ZombieDAO.Controllers; 

[Route("api/projects")]
public sealed class ProjectsController : ZombieDAOController {
    private readonly ProjectManager _projectManager;

    public ProjectsController(ProjectManager projectManager) {
        _projectManager = projectManager;
    }

    [HttpGet]
    public async Task<ActionResult<ProjectDetailsDTO[]>> GetAll(CancellationToken token) {
        return Ok(await _projectManager.GetAll(token));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDetailsDTO>> GetById([FromRoute] Guid id, CancellationToken token) {
        return Ok(await _projectManager.GetById(id, GetCurrentUser().Wallet, token));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDetailsDTO>> Create([FromBody] CreateProjectDTO dto, CancellationToken token) {
        var project = await _projectManager.Create(dto, GetCurrentUser().Wallet, token);
        return Created($"/projects/{project.ID}", project);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProjectDTO dto, CancellationToken token) {
        await _projectManager.Update(id, dto, GetCurrentUser().Wallet, token);
        return Accepted();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken token) {
        await _projectManager.Delete(id, GetCurrentUser().Wallet, token);
        return Accepted();
    }

    [HttpPost("{id:guid}/add_member")]
    public async Task<ActionResult> AddMember([FromRoute] Guid id, [FromBody] AddProjectMemberDTO dto, CancellationToken token) {
        await _projectManager.AddMember(id, dto, GetCurrentUser().Wallet, token);
        return Accepted();
    }

    [HttpPost("{id:guid}/remove_member")]
    public async Task<ActionResult> RemoveMember([FromRoute] Guid id, [FromBody] string member, CancellationToken token) {
        await _projectManager.RemoveMember(id, member, GetCurrentUser().Wallet, token);
        return Accepted();
    }
}
