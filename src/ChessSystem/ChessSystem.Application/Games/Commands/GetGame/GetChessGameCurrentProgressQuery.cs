namespace ChessSystem.Application.Games.Commands.GetGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessGameLogic;
    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;
    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities.Moves;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetChessGameCurrentProgressQuery : IRequest<ChessGame>
    {
        public GetChessGameCurrentProgressQuery(string whitePlayerId, string blackPlayerId)
        {
            this.WhitePlayerId = whitePlayerId;
            this.BlackPlayerId = blackPlayerId;
        }

        /// <summary>
        /// Gets or sets the White Player Id.
        /// </summary>
        public string WhitePlayerId { get; set; }

        /// <summary>
        /// Gets or sets the Black Player Id.
        /// </summary>
        public string BlackPlayerId { get; set; }

        public class GetChessGameCurrentProgressQueryHandler : IRequestHandler<GetChessGameCurrentProgressQuery, ChessGame>
        {
            private readonly IChessApplicationData chessApplicationData;

            public GetChessGameCurrentProgressQueryHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<ChessGame> Handle(GetChessGameCurrentProgressQuery request, CancellationToken cancellationToken)
            {
                var chessGameInstance = new ChessGame(
                    () => { return ChessFigureProductionType.Queen; },
                    (result) => { });

                Domain.Entities.ChessGame chessGameWithCastlingAndNormalMoves = await this.chessApplicationData
                    .ChessGames
                    .Include(game => game.CastlingMoves)
                        .ThenInclude(move => move.KingInitialPosition)
                    .Include(game => game.CastlingMoves)
                        .ThenInclude(move => move.RookInitialPosition)
                    .Include(game => game.NormalChessMoves)
                        .ThenInclude(move => move.InitialPosition)
                    .Include(game => game.NormalChessMoves)
                        .ThenInclude(move => move.TargetPosition)
                    .SingleAsync(game => game.WhitePlayerId == request.WhitePlayerId && game.BlackPlayerId == request.BlackPlayerId && game.EndGameInfo == null);

                var castlingMovesFromTheDatabase = chessGameWithCastlingAndNormalMoves
                    .CastlingMoves
                    .ToList();

                var normalMovesFromTheDatabase = chessGameWithCastlingAndNormalMoves
                    .NormalChessMoves
                    .ToList();

                List<BaseMove> moves = castlingMovesFromTheDatabase.Cast<BaseMove>().Concat(normalMovesFromTheDatabase.Cast<BaseMove>()).ToList();

                foreach (var move in moves.OrderBy(move => move.OrderInTheGame))
                {
                    if (move is CastlingMove)
                    {
                        var moveAsCastling = (CastlingMove)move;

                        chessGameInstance.MakeCastling(
                            moveAsCastling.KingInitialPosition.Horizontal,
                            moveAsCastling.KingInitialPosition.Vertical,
                            moveAsCastling.RookInitialPosition.Horizontal,
                            moveAsCastling.RookInitialPosition.Vertical,
                            Enum.Parse<ChessColors>(moveAsCastling.ColorOfTheFigures.ToString()));
                    }
                    else
                    {
                        var moveAsNormal = (NormalMove)move;

                        chessGameInstance.NormalMove(
                            moveAsNormal.InitialPosition.Horizontal,
                            moveAsNormal.InitialPosition.Vertical,
                            moveAsNormal.TargetPosition.Horizontal,
                            moveAsNormal.TargetPosition.Vertical,
                            Enum.Parse<ChessFigureType>(moveAsNormal.ChessFigureType.ToString()),
                            Enum.Parse<ChessColors>(moveAsNormal.ChessFigureColor.ToString()));
                    }
                }

                return chessGameInstance;
            }
        }
    }
}
