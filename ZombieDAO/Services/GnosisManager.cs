namespace ZombieDAO.Services; 

public sealed class GnosisManager {
    private readonly GnosisRepository _gnosisRepository;
    private readonly ProjectManager _projectManager;

    public GnosisManager(GnosisRepository gnosisRepository, ProjectManager projectManager) {
        _gnosisRepository = gnosisRepository;
        _projectManager = projectManager;
    }

    public async Task<GnosisSafeDetailsDTO> GetByID(Guid id, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(id, token);
        return GnosisSafeDetailsDTO.Create(safe, wallet);
    }

    public async Task<GnosisSafeDetailsDTO> CreateGnosisSafe(Guid projectID, CreateGnosisSafeDTO dto, string wallet, CancellationToken token) {
        var current = await _projectManager.GetById(projectID, wallet, token);
        if (current.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        var safe = GnosisSafeModel.Create(dto, projectID);
        var added = await _gnosisRepository.Create(safe, token);
        
        return GnosisSafeDetailsDTO.Create(added);
    }

    public async Task AddToken(Guid safeID, CreateGnosisSafeTokenDTO dto, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(safeID, token);
        var project = await _projectManager.GetById(safe.ProjectID, wallet, token);
        if (project.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        var model = new GnosisSafeTokenModel {
            Address = dto.Address, Symbol = dto.Symbol, SafeID = safe.ID
        };

        await _gnosisRepository.AddToken(model, token);
    }

    public async Task AddTransaction(Guid safeID, CreateGnosisSafeTransactionDTO dto, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(safeID, token);
        var project = await _projectManager.GetById(safe.ProjectID, wallet, token);
        if (project.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        var model = GnosisSafeTransactionModel.Create(dto, safeID, wallet);
        await _gnosisRepository.AddTransaction(model, token);
    }

    public async Task DeleteTransaction(Guid safeID, Guid id, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(safeID, token);
        var project = await _projectManager.GetById(safe.ProjectID, wallet, token);
        if (project.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();

        await _gnosisRepository.DeleteTransaction(id, token);
    }

    public async Task AddConfirmation(Guid safeID, Guid txID, CreateGnosisSafeConfirmationDTO dto, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(safeID, token);
        var project = await _projectManager.GetById(safe.ProjectID, wallet, token);
        if (project.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();
        var tx = safe.Transactions.Find(a => a.ID == txID);
        if (tx == null) throw new NotFoundException("Invalid transaction ID");
        
        var model = GnosisSafeConfirmationModel.Create(dto, txID, wallet);
        await _gnosisRepository.AddConfirmation(model, token);
    }

    public async Task SetTransactionExecuted(Guid safeID, Guid txID, string wallet, CancellationToken token) {
        var safe = await _gnosisRepository.GetByID(safeID, token);
        var project = await _projectManager.GetById(safe.ProjectID, wallet, token);
        if (project.Level is not ProjectMemberLevel.ADMIN) throw new NotAllowedException();
        await _gnosisRepository.SetExecuted(txID, token);
    }
}
