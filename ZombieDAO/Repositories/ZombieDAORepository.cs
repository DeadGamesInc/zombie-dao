namespace ZombieDAO.Repositories; 

public class ZombieDAORepository {
    protected readonly Serilog.ILogger _logger;
    protected readonly IDbContextFactory<DataContext> _factory;

    private readonly string _repositoryName;

    protected ZombieDAORepository(Serilog.ILogger logger, IDbContextFactory<DataContext> factory, string repositoryName) {
        _logger = logger;
        _factory = factory;
        _repositoryName = repositoryName;
    }
    
    protected async Task Executor(Func<Task> action, string operation) {
        async Task<int> wrappedAction() {
            await action();
            return 0;
        }

        await Executor(wrappedAction, operation);
    }
    
    protected async Task<T> Executor<T>(Func<Task<T>> action, string operation) {
        _logger.Debug("Executing database operation {Repository}.{Operation}", _repositoryName, operation);

        try {
            return await action();
        }
        catch (OperationCanceledException) {
            throw new InternalException();
        }
        catch (Exception error) {
            _logger.Error(error, "An exception occured during database operation {Repository}.{Operation}", _repositoryName, operation);
            throw;
        }
    }
}
