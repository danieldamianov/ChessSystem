namespace Infrastructure.Identity
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Application.Common.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class IdentityService : IIdentity
    {
        private readonly UserManager<ChessAppUser> userManager;

        public IdentityService(UserManager<ChessAppUser> userManager)
            => this.userManager = userManager;

        public async Task<string> GetUserNameAsync(string userId)
            => await this.userManager
                .Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

        public async Task<(Result Result, string UserId)> CreateUser(string userName, string password)
        {
            var user = new ChessAppUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await this.userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUser(string userId)
        {
            var user = this.userManager
                .Users
                .SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await this.DeleteUser(user);
            }

            return Result.Success;
        }

        public async Task<Result> DeleteUser(ChessAppUser user)
        {
            var result = await this.userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public string GetUserName(string userId)
            => this.userManager
                .Users
                .Where(u => u.Id == userId)
                .Select(u => u.UserName)
                .FirstOrDefault();
    }
}
