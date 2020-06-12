namespace ChessWebApplication.Controllers.Game.ViewModels
{
    using ChessGameLogic;

    public class GamePlayViewModel
    {
        public GamePlayViewModel(string whitePlayerId, string blackPlayerId, string playerColor)
        {
            this.WhitePlayerId = whitePlayerId;
            this.BlackPlayerId = blackPlayerId;
            this.PlayerColor = playerColor;
        }

        public string WhitePlayerId { get; set; }

        public string BlackPlayerId { get; set; }

        public string PlayerColor { get; set; }
    }
}
