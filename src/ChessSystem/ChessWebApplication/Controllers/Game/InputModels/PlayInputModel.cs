using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWebApplication.Controllers.Game.InputModels
{
    public class PlayInputModel
    {
        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public string PlayerColor { get; set; } // black or white
    }
}
