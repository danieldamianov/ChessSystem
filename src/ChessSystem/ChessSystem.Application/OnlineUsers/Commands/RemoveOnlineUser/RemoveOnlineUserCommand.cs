using ChessSystem.Application.Common.Interfaces;
using ChessSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessSystem.Application.OnlineUsers.Commands.RemoveOnlineUser
{
    public class RemoveOnlineUserCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public class RemoveOnlineUserCommandHandler : IRequestHandler<RemoveOnlineUserCommand,bool>
        {
            private readonly IChessApplicationData chessApplicationData;

            public RemoveOnlineUserCommandHandler(IChessApplicationData chessApplicationData)
            {
                this.chessApplicationData = chessApplicationData;
            }

            public async Task<bool> Handle(RemoveOnlineUserCommand request, CancellationToken cancellationToken)
            {
                this.chessApplicationData.LogedInUsers
                    .Remove(
                        this.chessApplicationData.LogedInUsers.Single(user => user.UserId == request.UserId)
                    );

                await this.chessApplicationData.SaveChanges(cancellationToken);

                return true;
            }
        }
    }
}
