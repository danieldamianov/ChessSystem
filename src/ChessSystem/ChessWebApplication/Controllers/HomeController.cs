namespace ChessWebApplication.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ChessSystem.Application.Common.Interfaces;
    using ChessWebApplication.Common;
    using ChessWebApplication.Hubs;
    using ChessWebApplication.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;

    public class HomeController : MediatorController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;

        public HomeController(ILogger<HomeController> logger,
            ICurrentUser currentUser)
        {
            this._logger = logger;
            this.currentUser = currentUser;
        }

        [Authorize]
        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult SeeOnlineUsers()
        {
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
