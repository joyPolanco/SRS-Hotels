using Microsoft.AspNetCore.Identity;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;

namespace SRS_Hotels.Data.src.Infraestructure.Identity.Seeds
{
    public static class UsersSeeder
    {
        public static async Task SeedAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            await CreateUserIfNotExists(userManager, "admin1@hotel.com", "Admin123!", "Admin One", "Administrator");
            await CreateUserIfNotExists(userManager, "admin2@hotel.com", "Admin123!", "Admin Two", "Administrator");

            await CreateUserIfNotExists(userManager, "reception@hotel.com", "Reception123!", "Reception User", "Receptionist");

            await CreateUserIfNotExists(userManager, "client1@hotel.com", "Client123!", "Client One", "Client");
            await CreateUserIfNotExists(userManager, "client2@hotel.com", "Client123!", "Client Two", "Client");
        }

        private static async Task CreateUserIfNotExists(
            UserManager<ApplicationUser> userManager,
            string email,
            string password,
            string fullName,
            string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);

            if (existingUser != null)
                return;

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = email,
                FullName = fullName,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception($"Error creando usuario {email}: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            await userManager.AddToRoleAsync(user, role);
        }
    }
}
