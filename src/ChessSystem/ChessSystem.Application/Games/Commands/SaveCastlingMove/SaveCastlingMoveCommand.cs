namespace ChessSystem.Application.Games.Commands.SaveCastlingMove
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Entities.Moves;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class SaveCastlingMoveCommand : IRequest<bool>
    {
        public SaveCastlingMoveCommand(string whitePlayerId, string blackPlayerId, SaveCastlingMoveInputModel saveCastlingMoveInputModel)
        {
            this.WhitePlayerId = whitePlayerId;
            this.BlackPlayerId = blackPlayerId;
            this.SaveCastlingMoveInputModel = saveCastlingMoveInputModel;
        }

        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public SaveCastlingMoveInputModel SaveCastlingMoveInputModel { get; set; }

        public class SaveNormalMoveCommandHandler : IRequestHandler<SaveCastlingMoveCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public SaveNormalMoveCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(SaveCastlingMoveCommand request, CancellationToken cancellationToken)
            {
                var game = await this.chessApplicationData.ChessGames
                    .OrderBy(game => game.Id)
                    .FirstAsync(game => game.WhitePlayerId == request.WhitePlayerId && game.BlackPlayerId == request.BlackPlayerId && game.EndGameInfo == null);

                var movesMadeInTheGame = this.chessApplicationData.NormalMoves.Where(move => move.ChessGameId == game.Id).Count() +
                    this.chessApplicationData.CastlingMoves.Where(move => move.ChessGameId == game.Id).Count() +
                    this.chessApplicationData.PawnProductionMoves.Where(move => move.ChessGameId == game.Id).Count();

                CastlingMove castlingMove = new CastlingMove(movesMadeInTheGame + 1);

                SaveCastlingMoveInputModel saveCastlingMoveInputModel = request.SaveCastlingMoveInputModel;

                castlingMove.KingInitialPosition = new ChessBoardPosition(saveCastlingMoveInputModel.KingInitialPositionHorizontal, saveCastlingMoveInputModel.KingInitialPositionVertical);
                castlingMove.RookInitialPosition = new ChessBoardPosition(saveCastlingMoveInputModel.RookInitialPositionHorizontal, saveCastlingMoveInputModel.RookInitialPositionVertical);

                castlingMove.ColorOfTheFigures = saveCastlingMoveInputModel.ColorOfTheFigures;

                game.CastlingMoves.Add(castlingMove);

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return true;
            }
        }
    }
}
