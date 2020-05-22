namespace ChessWebApplication.Controllers.Archive
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design.Serialization;
    using System.Text;

    public class GameReplayViewModel
    {
        public GameReplayViewModel(string gameId, string playerColor)
        {
            this.GameId = gameId;
            this.PlayerColor = playerColor;
        }

        public string GameId { get; set; }

        public string PlayerColor { get; set; }
    }
}
