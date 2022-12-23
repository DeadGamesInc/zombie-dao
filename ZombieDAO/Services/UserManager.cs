namespace ZombieDAO.Services; 

public sealed class UserManager {
    private readonly BlockchainManager _blockchainManager;
    private readonly UsersRepository _usersRepository;

    private readonly List<WebSession> _sessions = new();

    public UserManager(BlockchainManager blockchainManager, UsersRepository usersRepository) {
        _blockchainManager = blockchainManager;
        _usersRepository = usersRepository;
    }

    public UserDetailsDTO? CheckSessionID(Guid id) {
        var session = _sessions.FirstOrDefault(a => a.ID == id);
        if (session != null) session.LastAction = DateTime.UtcNow;
        return session?.User;
    }

    public void Logout(Guid id) {
        var session = _sessions.FirstOrDefault(a => a.ID == id);
        if (session != null) _sessions.Remove(session);
    }

    public void ClearStaleSessions() {
        var sessions = _sessions.Where(a => (DateTime.UtcNow - a.LastAction) > Globals.IDLE_SESSION_TIMEOUT).ToList();
        foreach (var session in sessions) _sessions.Remove(session);
    }

    public async Task<string> StartRegistration(string wallet, CancellationToken token) {
        if (await _usersRepository.Exists(wallet, token)) throw new ConflictException();
        return _blockchainManager.GetSigningChallenge(wallet);
    }

    public async Task<WebSession> CompleteRegistration(CreateUserDTO dto, CancellationToken token) {
        var valid = _blockchainManager.ValidateSignature(dto.Wallet, dto.Signature);
        if (!valid) throw new NotAllowedException();

        var user = UserModel.Create(dto);

        var added = await _usersRepository.Create(user, token);
        var session = new WebSession { User = UserDetailsDTO.Create(added) };
        _sessions.Add(session);
        return session;
    }

    public async Task<string> StartLogin(string wallet, CancellationToken token) {
        if (!await _usersRepository.Exists(wallet, token)) throw new NotAllowedException();
        return _blockchainManager.GetSigningChallenge(wallet);
    }

    public async Task<WebSession> CompleteLogin(CompleteLoginDTO dto, CancellationToken token) {
        var valid = _blockchainManager.ValidateSignature(dto.Wallet, dto.Signature);
        if (!valid) throw new NotAllowedException();

        var user = await _usersRepository.Get(dto.Wallet, token);
        var session = new WebSession { User = UserDetailsDTO.Create(user) };
        _sessions.Add(session);
        return session;
    }
}
