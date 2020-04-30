namespace ChessSystem.Application.OnlineUsers.Commands.AddOnlineUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using ChessSystem.Application.Common.Interfaces;
    using ChessSystem.Domain.Entities;
    using MediatR;

    public class AddOnlineUserCommand : IRequest
    {
        public AddOnlineUserCommand(string userId, string username)
        {
            UserId = userId;
            Username = username;
        }

        public string UserId { get; set; }

        public string Username { get; set; }

        public class AddOnlineUserCommandHandler : AsyncRequestHandler<AddOnlineUserCommand>
        {
            private readonly IChessApplicationData chessApplicationData;

            public AddOnlineUserCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            protected async override Task Handle(AddOnlineUserCommand request, CancellationToken cancellationToken)
            {
                OnlineUser onlineUser = new OnlineUser()
                {
                    UserId = request.UserId,
                    OnlineSince = DateTime.UtcNow,
                    Username = request.Username,
                };

                await this.chessApplicationData.LogedInUsers.AddAsync(onlineUser);

                await this.chessApplicationData.SaveChanges(cancellationToken);
            }
        }
    }
}
