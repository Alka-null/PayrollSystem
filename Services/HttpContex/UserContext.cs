﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services.HttpContex
{
    public class UserContext : IUserContext
    {
        public ClaimsPrincipal User { get; }

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext.User;
        }
    }
}
