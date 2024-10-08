﻿using LinkDev.Talabat.Core.Application.Abstraction.Contracts;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		public string? UserId { get; }

        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
		}
    }
}
