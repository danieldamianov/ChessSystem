namespace ChessWebApplication.Controllers.Game
{
    using System.Threading.Tasks;

    using ChessSystem.Application.Games.Commands.CreateGame;
    using ChessWebApplication.Common;
    using ChessWebApplication.Controllers.Game.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class GameController : MediatorController
    {
        [Authorize]
        public async Task<IActionResult> Play(PlayInputModel playInputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.Redirect("/");
            }

            if (playInputModel.PlayerColor == "white")
            {
                await this.Mediator.Send(new EnsureCreatedGameCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }

            return this.View(playInputModel);
        }
    }
}
