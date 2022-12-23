using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using ZombieDAO.Middlewares;

namespace ZombieDAO; 

public sealed class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddControllers().AddNewtonsoftJson(options => {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
        });
        
        services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");
    }

    public void Configure(IApplicationBuilder application, IWebHostEnvironment environment) {
        application.UseMiddleware<ErrorMonitor>();
        application.UseMiddleware<SessionInjector>();
        
        application.UseStaticFiles();
        application.UseRouting();
        
        application.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute("default", "api/{controller}/{action=Index}/{id?}");
        });
        
        application.UseSpa(spa => {
            spa.Options.SourcePath = "../UI";
            if (environment.IsDevelopment()) spa.UseReactDevelopmentServer("start");
        });
    }
}
