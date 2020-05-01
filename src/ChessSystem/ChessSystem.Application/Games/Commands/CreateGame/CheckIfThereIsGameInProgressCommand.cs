namespace ChessSystem.Application.Games.Commands.CreateGame
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Command that checks if there is game in progress with the given players.
    /// </summary>
    public class CheckIfThereIsGameInProgressCommand : IRequest<bool>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatedNewGameCommand"/> class.
        /// </summary>
        /// <param name="whitePlayerId">The Id of the white player.</param>
        /// <param name="blackPlayerId">The Id of the black player.</param>
        public CheckIfThereIsGameInProgressCommand(string whitePlayerId, string blackPlayerId)
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

        /// <summary>
        /// Handler for the command.
        /// </summary>
        public class CheckIfThereIsGameInProgressCommandHandler : IRequestHandler<CheckIfThereIsGameInProgressCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            /// <summary>
            /// Initializes a new instance of the <see cref="CheckIfThereIsGameInProgressCommandHandler"/> class.
            /// </summary>
            /// <param name="chessApplicationData">DI generetad.</param>
            public CheckIfThereIsGameInProgressCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            /// <summary>
            /// Handler.
            /// </summary>
            /// <param name="request">request.</param>
            /// <param name="cancellationToken">cancellationToken.</param>
            /// <returns>result.</returns>
            public async Task<bool> Handle(CheckIfThereIsGameInProgressCommand request, CancellationToken cancellationToken)
                => await this.chessApplicationData.ChessGames
                .AnyAsync(game => game.WhitePlayerId == request.WhitePlayerId && game.BlackPlayerId == request.BlackPlayerId && game.EndGameInfo == null);
        }
    }
}