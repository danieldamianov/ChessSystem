namespace ChessWebApplication.Controllers.Archive
{
    using ChessSystem.Application.Archive.Queries.PlayedGames;
    using ChessSystem.Application.Common.Interfaces;
    using ChessWebApplication.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ArchiveController : MediatorController
    {
        private readonly ICurrentUser currentUser;

        public ArchiveController(ICurrentUser currentUser)
        {
            this.currentUser = currentUser;
        }

        [Authorize]
        public async Task<IActionResult> Games()
        {
            var games = await this.Mediator.Send(new GetAllPlayedGamesOfUserQuery(this.currentUser.UserId));
            return this.View(games);
        }
    }
}
