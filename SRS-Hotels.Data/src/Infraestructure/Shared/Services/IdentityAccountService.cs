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
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException("Error al registrar el cliente: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Client");

            return user.Id;
        }

        public async Task<Guid> RegisterEmployeeAsync(
            string email,
            string password,
            string fullName,
            string phoneNumber)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = fullName,
                CreatedAt = DateTime.UtcNow,
                PhoneNumber = phoneNumber,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new ValidationException("Error al registrar el empleado: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            return user.Id;
        }

        public async Task<UpdateUserIdentityResponse> UpdateUserAsync(Guid id, string fullName)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return new UpdateUserIdentityResponse
                {
                    Success = false,
                    Message = "Usuario no encontrado",
                    UserBlocked = false
                };
            }

            if (user.LockoutEnd.HasValue &&
                user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                return new UpdateUserIdentityResponse
                {
                    Success = false,
                    Message = $"El usuario está bloqueado hasta {user.LockoutEnd.Value.UtcDateTime}",
                    UserBlocked = true
                };
            }

            user.FullName = fullName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UpdateUserIdentityResponse
                {
                    Success = false,
                    Message = "Error al actualizar el usuario",
                    UserBlocked = false
                };
            }

            return new UpdateUserIdentityResponse
            {
                Success = true,
                Message = "Usuario actualizado correctamente",
                UserBlocked = false
            };
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
                IsBlocked = false
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
                throw new Exception("Error al confirmar el correo electrónico");
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return await _userManager.GeneratePasswordResetTokenAsync(user!);
        }

        public async Task<ResetPasswordResponse> ChangePasswordAsync(
            Guid userId,
            string currentPassword,
            string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return new ResetPasswordResponse
                {
                    Successful = false,
                    Message = "Usuario no encontrado"
                };
            }

            if (user.LockoutEnd.HasValue &&
                user.LockoutEnd.Value > DateTimeOffset.UtcNow)
            {
                return new ResetPasswordResponse
                {
                    Successful = false,
                    UserIsBlocked = true,
                    Message = $"El usuario está bloqueado hasta {user.LockoutEnd.Value.UtcDateTime}"
                };
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                currentPassword,
                newPassword
            );

            if (!result.Succeeded)
            {
                return new ResetPasswordResponse
                {
                    Successful = false,
                    Message = "No se pudo cambiar la contraseña"
                };
            }

            return new ResetPasswordResponse
            {
                Successful = true,
                UserIsBlocked = false,
                Message = "Contraseña cambiada correctamente"
            };
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