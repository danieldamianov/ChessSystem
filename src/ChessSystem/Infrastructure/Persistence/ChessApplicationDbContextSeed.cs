using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class ChessApplicationDbContextSeed
    {
        public static async Task SeedAsync(
            ChessApplicationDbContext data,
            UserManager<ChessAppUser> userManager)
        {
            var defaultUser = new ChessAppUser
            {
                UserName = "admin@dev.com",
                Email = "admin@dev.com"
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Test1!");
            }

            await data.SaveChangesAsync();
        }
    }
}
