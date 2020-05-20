namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    using AutoMapper;
    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Entities.Moves;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAllPlayedGamesOfUserQuery : IRequest<List<PlayedGameOutputModel>>
    {
        public GetAllPlayedGamesOfUserQuery(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }

        public class GetAllPlayedGamesOfUserQueryHandler : IRequestHandler<GetAllPlayedGamesOfUserQuery, List<PlayedGameOutputModel>>
        {
            private readonly IChessApplicationData chessApplicationData;
            private readonly IIdentity identity;
            private readonly IMapper mapper;

            public GetAllPlayedGamesOfUserQueryHandler(
                IChessApplicationData chessApplicationData,
                IIdentity identity,
                IMapper mapper)
            {
                this.chessApplicationData = chessApplicationData;
                this.identity = identity;
                this.mapper = mapper;
            }

            public Task<List<PlayedGameOutputModel>> Handle(GetAllPlayedGamesOfUserQuery request, CancellationToken cancellationToken)
            {
                var games = this.chessApplicationData.ChessGames
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
                    .Where(game => (game.BlackPlayerId == request.UserId || game.WhitePlayerId == request.UserId) && game.EndGameInfo != null)
                    .ToList()
                    .Select(game => new PlayedGameOutputModel()
                    {
                        BlackPlayerName = this.identity.GetUserName(game.BlackPlayerId).GetAwaiter().GetResult(),
                        WhitePlayerName = this.identity.GetUserName(game.WhitePlayerId).GetAwaiter().GetResult(),
                        EndGameInfo = (EndGameInfo)game.EndGameInfo,
                        Moves = this.mapper.Map<List<NormalMove>, List<NormalMoveOutputModel>>(game.NormalChessMoves).Cast<BaseMoveOutputModel>().ToList().Concat(
                            this.mapper.Map<List<CastlingMove>, List<CastlingMoveOutputModel>>(game.CastlingMoves).Cast<BaseMoveOutputModel>().ToList()).Concat(
                            this.mapper.Map<List<PawnProductionMove>, List<ProductionMoveOutputModel>>(game.PawnProductionMoves).Cast<BaseMoveOutputModel>().ToList())
                            .OrderBy(move => move.OrderInTheGame).ToList(),
                        StartTime = game.StartedOn,
                        Duration = game.Duration,
                    })
                    .ToList();

                return Task.FromResult(games);
            }
        }
    }
}
