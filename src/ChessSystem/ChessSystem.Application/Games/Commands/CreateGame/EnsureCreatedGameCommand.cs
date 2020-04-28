namespace ChessSystem.Application.Games.Commands.CreateGame
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using MediatR;

    /// <summary>
    /// Command that handles creating a new game.
    /// </summary>
    public class EnsureCreatedGameCommand : IRequest<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnsureCreatedGameCommand"/> class.
        /// </summary>
        /// <param name="whitePlayerId">The Id of the white player.</param>
        /// <param name="blackPlayerId">The Id of the black player.</param>
        public EnsureCreatedGameCommand(string whitePlayerId, string blackPlayerId)
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
        public class EnsureCreatedGameCommandHandler : IRequestHandler<EnsureCreatedGameCommand, string>
        {
            private readonly IChessApplicationData chessApplicationData;

            /// <summary>
            /// Initializes a new instance of the <see cref="EnsureCreatedGameCommandHandler"/> class.
            /// </summary>
            /// <param name="chessApplicationData">DI generetad.</param>
            public EnsureCreatedGameCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            /// <summary>
            /// Handler.
            /// </summary>
            /// <param name="request">request.</param>
            /// <param name="cancellationToken">cancellationToken.</param>
            /// <returns>result.</returns>
            public async Task<string> Handle(EnsureCreatedGameCommand request, CancellationToken cancellationToken)
            {
                if (this.chessApplicationData.ChessGames.Any(game => game.WhitePlayerId == request.WhitePlayerId && game.BlackPlayerId == request.BlackPlayerId))
                {
                    return null;
                }

                ChessGame chessGame = new ChessGame(request.WhitePlayerId, request.BlackPlayerId);
                this.chessApplicationData.ChessGames.Add(chessGame);

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return chessGame.Id;
            }
        }
    }
}
