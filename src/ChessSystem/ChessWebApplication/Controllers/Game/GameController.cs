namespace ChessWebApplication.Controllers.Game
{
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

            ChessGame chessGameCurrentProgress = null;
            if (await this.Mediator.Send(new CheckIfThereIsGameInProgressCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId)))
            {
                chessGameCurrentProgress = await this.Mediator.Send(new GetChessGameCurrentProgressQuery(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }
            else
            {
                await this.Mediator.Send(new CreatedNewGameCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }

            var outputModel = new GamePlayViewModel(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId, playInputModel.PlayerColor, chessGameCurrentProgress);

            return this.View(outputModel);
        }
    }
}
