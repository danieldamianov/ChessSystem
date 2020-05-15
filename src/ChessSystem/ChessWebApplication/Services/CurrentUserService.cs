namespace ChessWebApplication.Services
{
    using System.Security.Claims;

    using ChessSystem.Application.Common.Interfaces;
    using Microsoft.AspNetCore.Http;

    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string UserId => this.httpContextAccessor
                .HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);
    }
}