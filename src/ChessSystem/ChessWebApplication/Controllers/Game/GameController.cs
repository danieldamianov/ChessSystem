using ChessSystem.Application.Games.Commands.CreateGame;
using ChessWebApplication.Common;
using ChessWebApplication.Controllers.Game.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessWebApplication.Controllers.Game
{
    public class GameController : MediatorController
    {
        [Authorize]
        public async Task<IActionResult> Play(PlayInputModel playInputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.Redirect("/");
            }

            if (playInputModel.PlayerColor == "white")
            {
                await this.Mediator.Send(new EnsureCreatedGameCommand(playInputModel.WhitePlayerId, playInputModel.BlackPlayerId));
            }

            return this.View(playInputModel);
        }
    }
}
