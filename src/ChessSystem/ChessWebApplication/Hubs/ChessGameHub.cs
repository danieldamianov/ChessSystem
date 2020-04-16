using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebApplication.Hubs
{
    public class ChessGameHub : Hub
    {
        public List<string> ClientUsernames = new List<string>();

        public async override Task OnConnectedAsync()
        {
            if (this.Context?.User?.Identity?.Name != null)
            {
                this.ClientUsernames.Add(this.Context.User.Identity.Name);

                await base.Clients.All.SendAsync("NewUser", this.Context.User.Identity.Name);
                foreach (var cliant in this.ClientUsernames)
                {
                    if (cliant != this.Context.User.Identity.Name)
                    {
                        await base.Clients.Caller.SendAsync("NewUser", cliant); 
                    }
                }
            }
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            if (this.Context?.User?.Identity?.Name != null)
            {
                this.ClientUsernames.Remove(this.Context.User.Identity.Name);

                await this.Clients.All.SendAsync("UserDisconnected", this.Context?.User?.Identity?.Name);
            }

        }
    }
}
