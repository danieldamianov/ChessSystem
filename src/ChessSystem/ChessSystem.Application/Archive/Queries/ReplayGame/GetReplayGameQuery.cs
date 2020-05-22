using AutoMapper;
using ChessSystem.Application.Archive.Queries.PlayedGames;
using ChessSystem.Application.Common.Interfaces;
using ChessSystem.Domain.Entities;
using ChessSystem.Domain.Entities.Moves;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessSystem.Application.Archive.Queries.ReplayGame
{
    public class GetReplayGameQuery : IRequest<PlayedGameOutputModel>
    {
        public GetReplayGameQuery(string gameId)
        {
            this.GameId = gameId;
        }

        public string GameId { get; set; }

        public class GetReplayGameQueryHandler : IRequestHandler<GetReplayGameQuery, PlayedGameOutputModel>
        {
            private readonly IChessApplicationData chessApplicationData;
            private readonly IMapper mapper;
            private readonly ICurrentUser currentUser;
            private readonly IIdentity identity;

            public GetReplayGameQueryHandler(
                IChessApplicationData chessApplicationData,
                IIdentity identity,
                IMapper mapper,
                ICurrentUser currentUser)
            {
                this.chessApplicationData = chessApplicationData;
                this.mapper = mapper;
                this.currentUser = currentUser;
                this.identity = identity;
            }

            public Task<PlayedGameOutputModel> Handle(GetReplayGameQuery request, CancellationToken cancellationToken)
            {
                var game = this.chessApplicationData.ChessGames
                    .Include(game => game.CastlingMoves)
                        .ThenInclude(move => move.KingInitialPosition)
                    .Include(game => game.CastlingMoves)
                        .ThenInclude(move => move.RookInitialPosition)
                    .Include(game => game.NormalChessMoves)
                        .ThenInclude(move => move.InitialPosition)
                    .Include(game => game.NormalChessMoves)
                        .ThenInclude(move => move.TargetPosition)
                    .Include(game => game.PawnProductionMoves)
                        .ThenInclude(move => move.ChessBoardPosition)
                    .Single(game => game.Id == request.GameId);

                var gameOutputModel = new PlayedGameOutputModel()
                {
                    Id = game.Id,
                    BlackPlayerName = this.identity.GetUserName(game.BlackPlayerId).GetAwaiter().GetResult(),
                    WhitePlayerName = this.identity.GetUserName(game.WhitePlayerId).GetAwaiter().GetResult(),
                    EndGameInfo = (EndGameInfo)game.EndGameInfo,
                    Moves = this.mapper.Map<List<NormalMove>, List<NormalMoveOutputModel>>(game.NormalChessMoves).Cast<BaseMoveOutputModel>().ToList().Concat(
                        this.mapper.Map<List<CastlingMove>, List<CastlingMoveOutputModel>>(game.CastlingMoves).Cast<BaseMoveOutputModel>().ToList()).Concat(
                        this.mapper.Map<List<PawnProductionMove>, List<ProductionMoveOutputModel>>(game.PawnProductionMoves).Cast<BaseMoveOutputModel>().ToList())
                        .OrderBy(move => move.OrderInTheGame).ToList(),
                    StartTime = game.StartedOn,
                    Duration = game.Duration,
                    PlayerColorInTheGame = this.currentUser.UserId == game.BlackPlayerId ?
                    ChessGameLogic.Enums.ChessColors.Black : ChessGameLogic.Enums.ChessColors.White,
                };

                return Task.FromResult(gameOutputModel);
            }
        }
    }
}
