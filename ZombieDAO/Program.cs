using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog.Events;

namespace ZombieDAO;

public sealed class Program {
    private static Serilog.ILogger? _logger;
    
    public static async Task Main() {
        if (!Directory.Exists(Globals.LOGS_DIR)) Directory.CreateDirectory(Globals.LOGS_DIR);
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3} {SourceContext}::{Message}{NewLine}{Exception}")
            .WriteTo.File(Path.Combine(Globals.LOGS_DIR, "zombie_dao_.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3} {SourceContext}::{Message}{NewLine}{Exception}")
            .CreateLogger();
        
        _logger = Log.Logger.ForContext<Program>();

        try {
            _logger.Information("Zombie DAO server host is starting up");
            var host = CreateHost();
            
            using (var scope = host.Services.CreateScope()) {
                var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
                var db = await factory.CreateDbContextAsync();
                await db.Database.MigrateAsync();
            }
            
            await host.RunAsync();
        }
        catch (HostAbortedException) {
            throw;
        }
        catch (Exception error) {
            if (error.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
            _logger.Error(error, "Exception occurred on Zombie DAO server host");
        }
        finally {
            await Log.CloseAndFlushAsync();
        }
    }

    private static IHost CreateHost() =>
        Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => {
                services.AddDbContextFactory<DataContext>(options => {
                    options.UseNpgsql(Environment.GetEnvironmentVariable("ZOMBIE_DAO_DBCONN") 
                                      ?? throw new Exception("Must provide PostgreSQL connection string"),
                        settings => settings.EnableRetryOnFailure(5, TimeSpan.FromSeconds(1), new List<string>()));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    
                    #if DEBUG
                        options.EnableDetailedErrors();
                        options.EnableSensitiveDataLogging();
                        options.ConfigureWarnings(warnings => {
                            warnings.Log(
                                CoreEventId.FirstWithoutOrderByAndFilterWarning, 
                                CoreEventId.RowLimitingOperationWithoutOrderByWarning);
                        });
                    #endif
                });

                services.AddSingleton<UsersRepository>();
                services.AddSingleton<ProjectsRepository>();
                services.AddSingleton<GnosisRepository>();
                
                services.AddSingleton<BlockchainManager>();
                
                services.AddSingleton<ProjectManager>();
                services.AddSingleton<GnosisManager>();
                services.AddSingleton<UserManager>();
                
                services.AddHostedService<SystemManager>();
            })
            .ConfigureWebHostDefaults(web => {
                web
                    .UseStartup<Startup>()
                    .UseUrls("http://*:5000")
                    .UseKestrel();
            })
            .UseSerilog()
            .Build();
}