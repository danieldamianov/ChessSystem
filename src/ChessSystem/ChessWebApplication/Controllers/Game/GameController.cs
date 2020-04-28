using ChessWebApplication.Controllers.Game.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWebApplication.Controllers.Game
{
    public class GameController : Controller
    {
        [Authorize]
        public IActionResult Play(PlayInputModel playInputModel)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.Redirect("/");
            }

            return this.View(playInputModel);
        }
    }
}
