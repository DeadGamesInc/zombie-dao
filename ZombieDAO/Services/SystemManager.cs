namespace ZombieDAO.Services; 

public sealed class SystemManager : IHostedService {
    private readonly Serilog.ILogger _logger = Log.Logger.ForContext<SystemManager>();

    private readonly BlockchainManager _blockchainManager;
    private readonly UserManager _userManager;

    private Timer? _periodicTimer;

    public SystemManager(BlockchainManager blockchainManager, UserManager userManager) {
        _blockchainManager = blockchainManager;
        _userManager = userManager;
    }

    public async Task StartAsync(CancellationToken cancellationToken) {
        await _blockchainManager.Initialize();
        _periodicTimer = new Timer(Fired_PeriodicTimer, null, Globals.PERIODIC_INTERVAL, Timeout.InfiniteTimeSpan);
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        _periodicTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
        return Task.CompletedTask;
    }

    private async void Fired_PeriodicTimer(object? state) {
        _logger.Information("Periodic timer has fired");
        _userManager.ClearStaleSessions();
        _periodicTimer = new Timer(Fired_PeriodicTimer, null, Globals.PERIODIC_INTERVAL, Timeout.InfiniteTimeSpan);
    }
}
