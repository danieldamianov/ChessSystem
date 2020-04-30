namespace ChessSystem.Application.OnlineUsers.Commands.RemoveOnlineUser
{
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using ChessSystem.Domain.Exceptions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class RemoveOnlineUserCommand : IRequest<bool>
    {
        public RemoveOnlineUserCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }

        public class RemoveOnlineUserCommandHandler : IRequestHandler<RemoveOnlineUserCommand, bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public RemoveOnlineUserCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(RemoveOnlineUserCommand request, CancellationToken cancellationToken)
            {
                OnlineUser onlineUser = await this.chessApplicationData.LogedInUsers
                    .SingleOrDefaultAsync(user => user.UserId == request.UserId);

                if (onlineUser == null)
                {
                    throw new CannotSetOfflineUserWhichIsOfflineException();
                }

                this.chessApplicationData.LogedInUsers.Remove(onlineUser);

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return true;
            }
        }
    }
}
