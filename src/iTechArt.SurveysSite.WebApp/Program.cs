using System.Threading.Tasks;
using iTechArt.SurveysSite.DomainModel;
using iTechArt.SurveysSite.Repositories.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace iTechArt.SurveysSite.WebApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();

            using (var serviceScope = host
                .Services
                .CreateScope())
            {
                await using var dbContext = serviceScope
                    .ServiceProvider
                    .GetRequiredService<SurveysSiteDbContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                await AddRoleAsync("admin", roleManager);
                await AddRoleAsync("user", roleManager);

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var admin = await userManager.FindByNameAsync("admin");

                if (admin == null)
                {
                    var role = await roleManager.FindByNameAsync("admin");
                    admin = new User
                    {
                        UserName = "admin",
                        Email = "admin123@mail.ru",
                        Role = role
                    };

                    await userManager.CreateAsync(admin, "admin123");
                }
            }

            await host.RunAsync();
        }


        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task AddRoleAsync(string roleName, RoleManager<Role> roleManager)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                return;
            }

            role = new Role
            {
                Name = roleName
            };

            await roleManager.CreateAsync(role);
        }
    }
}