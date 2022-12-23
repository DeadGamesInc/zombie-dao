namespace ZombieDAO.Repositories; 

public sealed class UsersRepository : ZombieDAORepository {
    public UsersRepository(IDbContextFactory<DataContext> factory) : base(Log.Logger.ForContext<UsersRepository>(), factory, "users") { }

    public async Task<UserModel> Get(string wallet, CancellationToken token) {
        async Task<UserModel> action() {
            _logger.Information("Getting user {Wallet}", wallet);
            await using var context = await _factory.CreateDbContextAsync(token);
            var user = await context.Users
                .Include(user => user.Projects).ThenInclude(project => project.Project)
                .FirstOrDefaultAsync(a => a.Wallet == wallet, token);
            if (user == null) throw new NotFoundException("Unknown user");
            return user;
        }

        return await Executor(action, "get");
    }
    
    public async Task<bool> Exists(string wallet, CancellationToken token) {
        async Task<bool> action() {
            _logger.Information("Checking if user {Wallet} exists", wallet);
            await using var context = await _factory.CreateDbContextAsync(token);
            var user = await context.Users.FirstOrDefaultAsync(a => a.Wallet == wallet, token);
            return user != null;
        }

        return await Executor(action, "exists");
    }
    
    public async Task<UserModel> Create(UserModel model, CancellationToken token) {
        async Task<UserModel> action() {
            _logger.Information("Creating user {Wallet}", model.Wallet);
            await using var context = await _factory.CreateDbContextAsync(token);
            await context.Users.AddAsync(model, token);
            await context.SaveChangesAsync(token);
            return model;
        }

        return await Executor(action, "create");
    }
}
