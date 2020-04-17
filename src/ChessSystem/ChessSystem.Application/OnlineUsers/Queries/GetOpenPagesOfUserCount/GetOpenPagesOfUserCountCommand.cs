//using ChessSystem.Application.Common.Interfaces;
//using ChessSystem.Domain.Entities;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ChessSystem.Application.OnlineUsers.Queries.GetOpenPagesOfUserCount
//{
//    public class GetOpenPagesOfUserCountCommand : IRequest<int>
//    {
//        public GetOpenPagesOfUserCountCommand(string userId)
//        {
//            UserId = userId;
//        }

//        public string UserId { get; set; }

//        public class GetOpenPagesOfUserCountCommandHandler : IRequestHandler<GetOpenPagesOfUserCountCommand, int>
//        {
//            private readonly IChessApplicationData chessApplicationData;

//            public GetOpenPagesOfUserCountCommandHandler(IChessApplicationData chessApplicationData)
//            {
//                this.chessApplicationData = chessApplicationData;
//            }

//            public async Task<int> Handle(GetOpenPagesOfUserCountCommand request, CancellationToken cancellationToken)
//            {
//                OnlineUser onlineUser = await this.chessApplicationData.LogedInUsers
//                    .SingleAsync(user => user.UserId == request.UserId);
//                return onlineUser.PagesOpened;
//            }
//        }

//    }
//}
