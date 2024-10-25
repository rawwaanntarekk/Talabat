using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Core.Application.Auth
{
    internal class AuthService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : IAuthService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO model)
        {
           var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
                throw new UnAuthorizedException("Invalid Login.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            // The account is not confirmed
            if (result.IsNotAllowed)
                throw new UnAuthorizedException("Account is not confirmed yet.");
            
            if(result.IsLockedOut)
                throw new UnAuthorizedException("Account is not locked.");

            // This case should be done with agreement of frontend and backend
            // if(result.RequiresTwoFactor)
            //    throw new UnAuthorizedException("Requires Two-Factor Authentication.");

            // The password is wrong
            if(!result.Succeeded)
                throw new UnAuthorizedException("Invalid Login.");

            var repsonse = new UserDTO
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = "Will be Done"
            };

            return repsonse;
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO model)
        {
            var user = new ApplicationUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                throw new ValidationException() { Errors = result.Errors.Select(E => E.Description) };

            var response =  new UserDTO
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = ""

            };

            return response;

        }
    }
}
