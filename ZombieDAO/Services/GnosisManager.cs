namespace ZombieDAO.Services; 

public class GnosisManager {
    private readonly GnosisRepository _gnosisRepository;
    private readonly ProjectManager _projectManager;

    public GnosisManager(GnosisRepository gnosisRepository, ProjectManager projectManager) {
        _gnosisRepository = gnosisRepository;
        _projectManager = projectManager;
    }

    public async Task<GnosisSafeDetailsDTO> GetByID(Guid id, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(id, token);
        return GnosisSafeDetailsDTO.Create(safe);
    }

    public async Task<GnosisSafeDetailsDTO> CreateGnosisSafe(Guid projectID, CreateGnosisSafeDTO dto, string wallet, CancellationToken token) {
        var current = await _projectManager.GetById(projectID, wallet, token);
        if (current.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        var safe = GnosisSafeModel.Create(dto, projectID);
        var added = await _gnosisRepository.Create(safe, token);
        
        return GnosisSafeDetailsDTO.Create(added);
    }
}
