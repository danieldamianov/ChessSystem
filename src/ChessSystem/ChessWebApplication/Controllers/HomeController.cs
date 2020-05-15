namespace ChessWebApplication.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Application.OnlineUsers.Queries.CheckIfUserIsOnline;
    using ChessWebApplication.Common;
    using ChessWebApplication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : MediatorController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
        {
            this._logger = logger;
            this.currentUser = currentUser;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> OnlineUsers()
        {
            if (await this.Mediator.Send(new CheckIfUsersIsOnlineCommand(this.currentUser.UserId)))
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
