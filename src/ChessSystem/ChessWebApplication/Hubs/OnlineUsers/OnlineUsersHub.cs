using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Abstractions;
using ChessSystem.Application.OnlineUsers.Commands.AddOnlineUser;
using ChessSystem.Application.Common.Interfaces;
using ChessSystem.Application.OnlineUsers.Queries.GetAllOnlineUsers;
using ChessSystem.Application.OnlineUsers.Commands.RemoveOnlineUser;
using ChessSystem.Application.OnlineUsers.Queries.CheckIfUserIsOnline;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ChessWebApplication.Controllers.Game.InputModels;

namespace ChessWebApplication.Hubs.OnlineUsers
{
    public class OnlineUsersHub : Hub
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICurrentUser currentUser;
        private readonly IMapper mapper;
        private IMediator mediator;

        public OnlineUsersHub(IHttpContextAccessor httpContextAccessor,
            ICurrentUser currentUser,
            IMapper mapper)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.currentUser = currentUser;
            this.mapper = mapper;
        }

        protected IMediator Mediator
            => this.mediator ??= (IMediator)this.httpContextAccessor.HttpContext
                .RequestServices
                .GetService(typeof(IMediator));

        public async override Task OnConnectedAsync()
        {
            await this.Mediator.Send(new AddOnlineUserCommand(this.currentUser.UserId, this.Context.User.Identity.Name));

            await this.Clients.All.SendAsync("NewUser", new OnlineUserSocketModel(this.Context.User.Identity.Name, this.currentUser.UserId));

            GetAllOnlineUsersOutputModel getAllOnlineUsersOutputModel = await this.mediator.Send(new GetAllOnlineUsersQuery());
            foreach (var client in getAllOnlineUsersOutputModel.OnlineUsers)
            {
                if (client.UserId != this.currentUser.UserId)
                {
                    await this.Clients.Caller.SendAsync("NewUser", new OnlineUserSocketModel(client.Username, client.UserId));
                }
            }
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await this.Mediator.Send(new RemoveOnlineUserCommand(this.currentUser.UserId));

            await this.Clients.All.SendAsync("UserDisconnected", new OnlineUserSocketModel(this.Context.User.Identity.Name,
            this.currentUser.UserId));

        }

        [Authorize]
        public async Task InviteUserToPlay(string invitedId)
        {
            await this.Clients.User(invitedId).SendAsync("HandleInvitation", new OnlineUserSocketModel
                (this.Context.User.Identity.Name, this.currentUser.UserId)) ;
        }

        [Authorize]
        public async Task AcceptGame(string opponentId)
        {
            var modelStartGameAsBlack = new PlayInputModel()
            {
                BlackPlayerId = this.currentUser.UserId,
                WhitePlayerId = opponentId,
            };

            var modelStartGameAsWhite = new PlayInputModel()
            {
                WhitePlayerId = this.currentUser.UserId,
                BlackPlayerId = opponentId,
            };
            await this.Clients.Caller.SendAsync("StartGameAsBlack", modelStartGameAsBlack);
            await this.Clients.User(opponentId).SendAsync("StartGameAsWhite", modelStartGameAsWhite);
        }
    }
}
