namespace ChessSystem.Application.Games.Commands.SaveMove
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Entities.Moves;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class SaveNormalMoveCommand : IRequest<bool>
    {
        public SaveNormalMoveCommand(string whitePlayerId, string blackPlayerId, SaveNormalMoveInputModel saveNormalMoveInput)
        {
            this.WhitePlayerId = whitePlayerId;
            this.BlackPlayerId = blackPlayerId;
            this.SaveNormalMoveInputModel = saveNormalMoveInput;
        }

        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public SaveNormalMoveInputModel SaveNormalMoveInputModel { get; set; }

        public class SaveNormalMoveCommandHandler : IRequestHandler<SaveNormalMoveCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public SaveNormalMoveCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(SaveNormalMoveCommand request, CancellationToken cancellationToken)
            {
                var game = await this.chessApplicationData.ChessGames
                    .OrderBy(game => game.Id)
                    .FirstAsync(game => game.WhitePlayerId == request.WhitePlayerId && game.BlackPlayerId == request.BlackPlayerId && game.EndGameInfo == null);

                var movesMadeInTheGame = this.chessApplicationData.NormalMoves.Where(move => move.ChessGameId == game.Id).Count() +
                    this.chessApplicationData.CastlingMoves.Where(move => move.ChessGameId == game.Id).Count() +
                    this.chessApplicationData.PawnProductionMoves.Where(move => move.ChessGameId == game.Id).Count();

                NormalMove normalMove = new NormalMove(
                    movesMadeInTheGame + 1,
                    request.SaveNormalMoveInputModel.ChessFigureType,
                    request.SaveNormalMoveInputModel.ChessFigureColor);

                normalMove.InitialPosition = new ChessBoardPosition(request.SaveNormalMoveInputModel.InitialPositionHorizontal, request.SaveNormalMoveInputModel.InitialPositionVertical);
                normalMove.TargetPosition = new ChessBoardPosition(request.SaveNormalMoveInputModel.TargetPositionHorizontal, request.SaveNormalMoveInputModel.TargetPositionVertical);

                game.NormalChessMoves.Add(normalMove);

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return true;
            }
        }
    }
}
