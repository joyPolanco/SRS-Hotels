using Microsoft.AspNetCore.Identity;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;

namespace SRS_Hotels.Data.src.Infraestructure.Identity.Seeds
{
    public static class IdentitySeederRunner
    {
        public static async Task RunAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await RoleSeeder.SeedAsync(roleManager);
        }
    }
}
