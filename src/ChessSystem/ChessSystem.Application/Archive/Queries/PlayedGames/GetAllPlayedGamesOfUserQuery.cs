namespace ChessSystem.Application.Archive.Queries.PlayedGames
{
    using ChessSystem.Application.Common.Interfaces;
    using MediatR;
    using System.Collections.Generic;
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

            public GetAllPlayedGamesOfUserQueryHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public Task<List<PlayedGameOutputModel>> Handle(GetAllPlayedGamesOfUserQuery request, CancellationToken cancellationToken)
            {
                
            }
        }
    }
}
