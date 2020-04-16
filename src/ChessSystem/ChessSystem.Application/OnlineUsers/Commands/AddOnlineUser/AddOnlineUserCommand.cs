using ChessSystem.Application.Common.Interfaces;
using ChessSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessSystem.Application.OnlineUsers.Commands.AddOnlineUser
{
    public class AddOnlineUserCommand : IRequest
    {
        public string UserId { get; set; }

        public class AddOnlineUserCommandHandler : AsyncRequestHandler<AddOnlineUserCommand>
        {
            private readonly IChessApplicationData chessApplicationData;

            public AddOnlineUserCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            protected async override Task Handle(AddOnlineUserCommand request, CancellationToken cancellationToken)
            {
                this.chessApplicationData.LogedInUsers.Add(new OnlineUser()
                {
                    UserId = request.UserId,
                    OnlineSince = DateTime.UtcNow,
                });

                await this.chessApplicationData.SaveChanges(cancellationToken);
            }
        }
    }
}
