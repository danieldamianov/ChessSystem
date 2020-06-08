namespace ChessWebApplication.Controllers.Game
{
    using System;
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

            ChessGame chessGameCurrentProgress = new ChessGame(
                () => throw new NotImplementedException(),
                async (result) => { await Task.CompletedTask; });

            if (await this.Mediator.Send(new CheckIfThereIsGameInProgressCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId)))
            {
                chessGameCurrentProgress = await this.Mediator.Send(new GetChessGameCurrentProgressQuery(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }
            else if (playInputModel.PlayerColor == "white")
            {
                await this.Mediator.Send(new CreatedNewGameCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }

            typeof(ChessGame).GetField("chooseFigureToProduceFunction", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(chessGameCurrentProgress, null);
            typeof(ChessGame).GetField("endGameHandleFunction", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(chessGameCurrentProgress, null);

            /*ChessBoardComponentModel chessBoardComponentModel = new ChessBoardComponentModel();

            for (char hor = 'a'; hor <= 'h'; hor++)
            {
                for (int ver = 1; ver <= 8; ver++)
                {
                    chessBoardComponentModel.board[hor][ver] = chessGameCurrentProgress.GetFigureOnPositionInfo(hor, ver);
                }
            }

            chessBoardComponentModel.ChessGameProgressInfo = chessGameCurrentProgress.GameProgressInfo;

            chessBoardComponentModel.PlayerOnTurn = chessGameCurrentProgress.PlayerOnTurn;*/

            var outputModel = new GamePlayViewModel(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId, playInputModel.PlayerColor, chessGameCurrentProgress);

            return this.View(outputModel);
        }
    }
}
