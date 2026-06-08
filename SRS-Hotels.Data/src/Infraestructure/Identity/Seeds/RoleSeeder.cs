using Microsoft.AspNetCore.Identity;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;

namespace SRS_Hotels.Data.src.Infraestructure.Identity.Seeds
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> roleManager)
        {
            string[] roles =
            {
                "Administrator",
                "Receptionist",
                "Client"
            };

            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role);

                if (!exists)
                {
                    await roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = role
                    });
                }
            }
        }
    }
}