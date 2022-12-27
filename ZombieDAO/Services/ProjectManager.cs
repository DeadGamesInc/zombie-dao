namespace ZombieDAO.Services; 

public sealed class ProjectManager {
    private readonly ProjectsRepository _projectsRepository;

    public ProjectManager(ProjectsRepository projectsRepository) {
        _projectsRepository = projectsRepository;
    }

    public async Task<ProjectDetailsDTO[]> GetAll(CancellationToken token) {
        var projects = await _projectsRepository.GetAll(token);
        return projects.Select(project => ProjectDetailsDTO.Create(project)).ToArray();
    }

    public async Task<ProjectDetailsDTO> GetById(Guid id, string wallet, CancellationToken token) {
        var project = await _projectsRepository.GetByID(id, token);
        var check = project.Members.FirstOrDefault(a => a.UserWallet == wallet);
        return check != null ? ProjectDetailsDTO.Create(project, true, check.Level) : ProjectDetailsDTO.Create(project);
    }

    public async Task<ProjectDetailsDTO> Create(CreateProjectDTO dto, string wallet, CancellationToken token) {
        var project = ProjectModel.Create(dto);
        var added = await _projectsRepository.Create(project, token);

        var member = new ProjectMemberModel {
            ProjectID = added.ID, UserWallet = wallet, Level = ProjectMemberLevel.ADMIN
        };

        await _projectsRepository.AddMember(member, token);
        return ProjectDetailsDTO.Create(added, true, ProjectMemberLevel.ADMIN);
    }

    public async Task Update(Guid id, UpdateProjectDTO dto, string wallet, CancellationToken token) {
        var current = await GetById(id, wallet, token);
        if (current.Level == null && current.Level != ProjectMemberLevel.ADMIN) throw new NotAllowedException();
        await _projectsRepository.Update(id, dto, token);
    }

    public async Task Delete(Guid id, string wallet, CancellationToken token) {
        var current = await GetById(id, wallet, token);
        if (current.Level == null && current.Level != ProjectMemberLevel.ADMIN) throw new NotAllowedException();
        await _projectsRepository.Delete(id, token);
    }

    public async Task AddMember(Guid id, AddProjectMemberDTO dto, string wallet, CancellationToken token) {
        var current = await GetById(id, wallet, token);
        if (current.Level == null && current.Level != ProjectMemberLevel.ADMIN) throw new NotAllowedException();
        
        var member = new ProjectMemberModel {
            ProjectID = id, UserWallet = dto.Wallet, Level = dto.Level
        };

        await _projectsRepository.AddMember(member, token);
    }

    public async Task RemoveMember(Guid id, string member, string wallet, CancellationToken token) {
        var current = await GetById(id, wallet, token);
        if (current.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        await _projectsRepository.RemoveMember(id, member, token);
    }
}
