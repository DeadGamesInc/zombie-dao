namespace ZombieDAO.Repositories; 

public sealed class GnosisRepository : ZombieDAORepository {
    public GnosisRepository(IDbContextFactory<DataContext> factory) : base(Log.Logger.ForContext<GnosisRepository>(), factory, "gnosis") { }

    public async Task<GnosisSafeModel> GetByID(Guid id, CancellationToken token) {
        async Task<GnosisSafeModel> action() {
            _logger.Information("Retrieving Gnosis safe {Id}", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var safe = await context.GnosisSafes
                .Include(a => a.Tokens)
                .Include(a => a.Transactions).ThenInclude(a => a.Confirmations)
                .FirstOrDefaultAsync(a => a.ID == id, token);
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

    public async Task AddToken(GnosisSafeTokenModel model, CancellationToken token) {
        async Task action() {
            _logger.Information("Adding token to Gnosis safe");
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.GnosisSafeTokens.AddAsync(model, token);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "add_token");
    }

    public async Task AddTransaction(GnosisSafeTransactionModel model, CancellationToken token) {
        async Task action() {
            _logger.Information("Adding Gnosis safe transaction");
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.GnosisSafeTransactions.AddAsync(model, token);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "add_transaction");
    }

    public async Task DeleteTransaction(Guid id, CancellationToken token) {
        async Task action() {
            _logger.Information("Deleting Gnosis safe transaction");
            await using var context = await _factory.CreateDbContextAsync(token);
            var tx = await context.GnosisSafeTransactions.FirstOrDefaultAsync(a => a.ID == id, token);
            if (tx == null) throw new NotFoundException("Invalid transaction ID");
            context.GnosisSafeTransactions.Remove(tx);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "remove_transaction");
    }

    public async Task AddConfirmation(GnosisSafeConfirmationModel model, CancellationToken token) {
        async Task action() {
            _logger.Information("Creating Gnosis transaction confirmation");
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.GnosisSafeConfirmations.AddAsync(model, token);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "add_confirmation");
    }

    public async Task SetExecuted(Guid id, CancellationToken token) {
        async Task action() {
            _logger.Information("Setting transaction {ID} to executed", id);
            await using var context = await _factory.CreateDbContextAsync(token);
            var transaction = await context.GnosisSafeTransactions.FirstOrDefaultAsync(a => a.ID == id, token);
            if (transaction == null) throw new NotFoundException("Invalid transaction ID");
            transaction.Executed = true;
            context.Update(transaction);
            await context.SaveChangesAsync(token);
        }

        await Executor(action, "set_transaction_executed");
    }
}
