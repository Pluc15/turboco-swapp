using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TurboCoConsole.Data;
using TurboCoConsole.Hubs;

namespace TurboCoConsole
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services
        )
        {
            services.AddMvc();
            services.AddSignalR();
            services.AddScoped<RepositoryFactory>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env
        )
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<DashboardHub>("/hub/dashboard");
                }
            );
        }
    }
}