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

namespace ChessWebApplication.Hubs
{
    public class ChessGameHub : Hub
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICurrentUser currentUser;
        private IMediator mediator;

        public ChessGameHub(IHttpContextAccessor httpContextAccessor,
            ICurrentUser currentUser)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.currentUser = currentUser;
        }

        protected IMediator Mediator
            => this.mediator ??= (IMediator)this.httpContextAccessor.HttpContext
                .RequestServices
                .GetService(typeof(IMediator));

        public async override Task OnConnectedAsync()
        {
            if (this.Context?.User?.Identity?.Name != null)
            {
                //this.ClientUsernames.Add(this.Context.User.Identity.Name);

                await this.Mediator.Send(new AddOnlineUserCommand() { UserId = this.currentUser.UserId, Username = this.Context.User.Identity.Name });

                await base.Clients.All.SendAsync("NewUser", this.Context.User.Identity.Name);
                GetAllOnlineUsersOutputModel getAllOnlineUsersOutputModel = await this.mediator.Send(new GetAllOnlineUsersQuery());
                foreach (var client in getAllOnlineUsersOutputModel.OnlineUsers)
                {
                    if (client.Username != this.Context.User.Identity.Name)
                    {
                        await base.Clients.Caller.SendAsync("NewUser", client.Username); 
                    }
                }
            }
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            if (this.Context?.User?.Identity?.Name != null)
            {
                //this.ClientUsernames.Remove(this.Context.User.Identity.Name);

                await this.Clients.All.SendAsync("UserDisconnected", this.Context?.User?.Identity?.Name);
            }

        }
    }
}
