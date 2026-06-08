using Microsoft.AspNetCore.Identity;
using SRS_Hotels.Data.src.BuildingBlocks.Abstractions;
using SRS_Hotels.Data.src.BuildingBlocks.Contracts;
using SRS_Hotels.Data.src.Infraestructure.Identity.Entities;
using System.ComponentModel.DataAnnotations;

namespace SRS_Hotels.Data.src.Infraestructure.Shared.Services
{
    public class IdentityAccountService : IIdentityAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityAccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Guid> RegisterClientAsync(
           string email,
           string password,
           string fullName,
           string phoneNumber)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = email,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true//TO-DO,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.Description)));

           
            await _userManager.AddToRoleAsync(user, "Client");

            return user.Id;
        }

        public async Task<Guid> RegisterEmployeeAsync(string email, string password, string fullName, string phoneNumber)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                UserName = email,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = phoneNumber,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return user.Id;
        }

        public async Task<User?> CheckPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            if (user.LockoutEnd.HasValue &&
                user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                return new User
                {
                    Email = user.Email ?? "",
                    FullName = user.FullName,
                    Id = user.Id,
                    IsBlocked = true,
                    BlockEnd = user.LockoutEnd
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                password,
                lockoutOnFailure: false);

            if (!result.Succeeded)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new User
            {
                Id = user.Id,
                Email = user.Email ?? "",
                FullName = user.FullName,
                Roles = roles.ToList(),
                IsBlocked = false,
            };
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GenerateEmailConfirmationTokenAsync(user!);
        }

        public async Task ConfirmEmailAsync(Guid userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.ConfirmEmailAsync(user!, token);

            if (!result.Succeeded)
                throw new Exception("Email confirmation failed");
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GeneratePasswordResetTokenAsync(user!);
        }

        public async Task ResetPasswordAsync(Guid userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.ResetPasswordAsync(user!, token, newPassword);

            if (!result.Succeeded)
                throw new Exception("Password reset failed");
        }

        public async Task<Guid?> GetUserIdByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user?.Id;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<IList<string>> GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GetRolesAsync(user!);
        }

        public async Task AddToRoleAsync(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.AddToRoleAsync(user!, role);
        }
    }
}