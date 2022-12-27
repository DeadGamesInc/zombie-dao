namespace ZombieDAO.Repositories; 

public sealed class GnosisRepository : ZombieDAORepository {
    public GnosisRepository(IDbContextFactory<DataContext> factory) : base(Log.Logger.ForContext<GnosisRepository>(), factory, "gnosis") { }

    public async Task<GnosisSafeModel> GetByID(Guid id, CancellationToken token) {
        async Task<GnosisSafeModel> action() {
            _logger.Information("Retrieving Gnosis safe {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var safe = await context.GnosisSafes.FirstOrDefaultAsync(a => a.ID == id, token);
            if (safe == null) throw new NotFoundException("Invalid Gnosis safe ID");
            return safe;
        }

        return await Executor(action, "get_by_id");
    }

    public async Task<GnosisSafeModel> Create(GnosisSafeModel model, CancellationToken token) {
        async Task<GnosisSafeModel> action() {
            _logger.Information("Creating Gnosis safe {Name} on {ChainID}", model.Name, model.ChainID);
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.GnosisSafes.AddAsync(model, token);
            await context.SaveChangesAsync(token);
            return model;
        }

        return await Executor(action, "create");
    }
}
