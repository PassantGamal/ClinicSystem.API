using ClinicSystem.Domain.IdentityModules;
using ClinicSystem.ServicesAbstration.Contracts.Services;
using ClinicSystem.Shared.CommonResult;
using ClinicSystem.Shared.DataTransferObject.IdentityDTOS;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClinicSystem.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                return Result<UserDTO>.Fail(Error.InvalidCredentials("User.InvalidCredentials"));

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
                return Result<UserDTO>.Fail(Error.InvalidCredentials("User.InvalidCredentials"));

            var token = await _tokenService.GenerateTokenAsync(user);
            return Result<UserDTO>.Ok(new UserDTO(user.Email!, user.DisplayName, token));
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName
            };

            var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
                return Result<UserDTO>.Fail(identityResult.Errors.Select(e => Error.Validation(e.Code, e.Description)).ToList());

            var role = registerDTO.Role?.Trim() ?? "Patient";
            var validRoles = new[] { "Admin", "Doctor", "Receptionist", "Patient" };
            if (!validRoles.Contains(role))
                role = "Patient";

            await _userManager.AddToRoleAsync(user, role);

            var token = await _tokenService.GenerateTokenAsync(user);
            return Result<UserDTO>.Ok(new UserDTO(user.Email!, user.DisplayName, token));
        }
    }
}