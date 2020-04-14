using ChessSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ChessWebApplication.Services
{
    public class CurrentUserService : ICurrentUser
    {

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => this.UserId = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

        public string UserId { get; }
    }
}

