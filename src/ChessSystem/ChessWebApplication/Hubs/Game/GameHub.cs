using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebApplication.Hubs.Game
{
    public class GameHub : Hub
    {
        public async Task UserHasMadeMove(
            string opponentId,
            string initialPositionHorizontal,
            string initialPositionVertical,
            string targetPositionHorizontal,
            string targetPositionVertical,
            string figureType,
            string figureColor)
        {
            await this.Clients.User(opponentId).SendAsync(
                "OpponentHasMadeMove",
                opponentId,
                initialPositionHorizontal,
                initialPositionVertical,
                targetPositionHorizontal,
                targetPositionVertical,
                figureType,
                figureColor);
        }
    }
}
