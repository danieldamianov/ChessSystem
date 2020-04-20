using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebApplication.Hubs.Game
{
    public class GameHub : Hub
    {
        public async Task UserHasMadeMove(string opponentId)
        {
            await this.Clients.User(opponentId).SendAsync("OpponentHasMadeMove");
        }
    }
}
