using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager _serviceManager) : BaseAPIController
    {
        [HttpPost("login")] // POST: /api/account/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var result = await _serviceManager.AuthService.LoginAsync(model);
            return Ok(result);
        }
        
        [HttpPost("register")] // POST: /api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var result = await _serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }
    }
}
