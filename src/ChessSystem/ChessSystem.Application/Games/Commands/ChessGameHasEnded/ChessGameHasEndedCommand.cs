namespace ChessSystem.Application.Games.Commands.ChessGameHasEnded
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ChessGameHasEndedCommand : IRequest<bool>
    {
        private readonly string whitePlayerId;
        private readonly string blackPlayerId;
        private readonly EndGameInfo endGameInfo;

        public ChessGameHasEndedCommand(string whitePlayerId, string blackPlayerId, EndGameInfo endGameInfo)
        {
            this.whitePlayerId = whitePlayerId;
            this.blackPlayerId = blackPlayerId;
            this.endGameInfo = endGameInfo;
        }

        public class ChessGameHasEndedCommandHandler : IRequestHandler<ChessGameHasEndedCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public ChessGameHasEndedCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(ChessGameHasEndedCommand request, CancellationToken cancellationToken)
            {
                var game = await this.chessApplicationData.ChessGames.SingleAsync(game => game.WhitePlayerId == request.whitePlayerId
                && game.BlackPlayerId == request.blackPlayerId && game.EndGameInfo == null);

                game.EndGameInfo = request.endGameInfo;
                game.Duration = DateTime.UtcNow.Subtract(game.StartedOn);

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return true;
            }
        }
    }
}
