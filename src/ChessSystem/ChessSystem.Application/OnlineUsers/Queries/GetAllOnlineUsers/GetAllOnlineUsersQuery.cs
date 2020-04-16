using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChessSystem.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers
{
    public class GetAllOnlineUsersQuery : IRequest<GetAllOnlineUsersOutputModel>
    {
        public class GetAllOnlineUsersQueryHandler : IRequestHandler<GetAllOnlineUsersQuery, GetAllOnlineUsersOutputModel>
        {
            private readonly IChessApplicationData chessApplicationData;
            private readonly IMapper mapper;

            public GetAllOnlineUsersQueryHandler(IChessApplicationData chessApplicationData,
                IMapper mapper)
            {
                this.chessApplicationData = chessApplicationData;
                this.mapper = mapper;
            }

            public async Task<GetAllOnlineUsersOutputModel> Handle(GetAllOnlineUsersQuery request, CancellationToken cancellationToken)
            {
                var outputModel = new GetAllOnlineUsersOutputModel();

                outputModel.OnlineUsers = await this.chessApplicationData.
                    LogedInUsers.ProjectTo<OnlineUserOutputModel>(this.mapper.ConfigurationProvider).ToListAsync();

                return outputModel;
            }
        }
    }
}
