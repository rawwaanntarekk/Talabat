using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Core.Application.Auth
{
    public class AuthService(UserManager<ApplicationUser> _userManager,
                             SignInManager<ApplicationUser> _signInManager,
                             IOptions<JwtSettings> _jwtSettings) : IAuthService
    {
        private readonly JwtSettings _jwtSettings = _jwtSettings.Value;
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
                Token = await GenerateTokenAsync(user)
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
                Token = await GenerateTokenAsync(user),

            };

            return response;

        }

        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var rolesAsClaims = new List<Claim>();

            foreach (var role in roles)
                rolesAsClaims.Add(new(ClaimTypes.Role, role));

           
            var Claims = new List<Claim>()
            {
                // Registered Claims
                new(ClaimTypes.PrimarySid, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.GivenName, user.DisplayName),
                // Private Claims
                new("Secret", "Secret Information")


            }
             .Union(userClaims)
             .Union(rolesAsClaims);

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signInCredentails = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256);


            var tokenObj = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: Claims,
                signingCredentials: signInCredentails
             );

            return new JwtSecurityTokenHandler().WriteToken(tokenObj);
                 

        }
    }
}
