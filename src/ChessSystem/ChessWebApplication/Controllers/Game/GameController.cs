namespace ChessWebApplication.Controllers.Game
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using ChessGameLogic;
    using ChessSystem.Application.Games.Commands.CreateGame;
    using ChessSystem.Application.Games.Commands.GetGame;
    using ChessWebApplication.Common;
    using ChessWebApplication.Controllers.Game.InputModels;
    using ChessWebApplication.Controllers.Game.ViewModels;
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

            if (await this.TheOpponentCodeHasAlreadyCreatedTheGameOrThereIsGameInProgressAlready(playInputModel))
            {
                await this.Mediator.Send(new CreatedNewGameCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }

            var outputModel = new GamePlayViewModel(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId, playInputModel.PlayerColor);

            return this.View(outputModel);
        }

        private async Task<bool> TheOpponentCodeHasAlreadyCreatedTheGameOrThereIsGameInProgressAlready(PlayInputModel playInputModel)
        {
            return await this.Mediator.Send(new CheckIfThereIsGameInProgressCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId)) == false;
        }
    }
}
