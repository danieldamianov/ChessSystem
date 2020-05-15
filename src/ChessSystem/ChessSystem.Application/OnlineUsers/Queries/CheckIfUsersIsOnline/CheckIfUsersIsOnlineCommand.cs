namespace ChessSystem.Application.OnlineUsers.Queries.CheckIfUserIsOnline
{
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CheckIfUsersIsOnlineCommand : IRequest<bool>
    {
        public CheckIfUsersIsOnlineCommand(string userId)
        {
            this.UserId = userId;
        }

        public string UserId { get; set; }

        public class CheckIfUserIsOnlineCommandHandler : IRequestHandler<CheckIfUsersIsOnlineCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public CheckIfUserIsOnlineCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(CheckIfUsersIsOnlineCommand request, CancellationToken cancellationToken)
                => await this.chessApplicationData.LogedInUsers.AnyAsync(user => user.UserId == request.UserId);
        }
    }
}
