namespace ChessWebApplication
{
    using System.Collections.Generic;

    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;

    public class ChessBoardComponentModel
    {
        public ChessBoardComponentModel()
        {
            this.board = new Dictionary<char, Dictionary<int, ChessFigureOnPositionInfo>>();

            for (char i = 'a'; i <= 'h'; i++)
            {
                this.board[i] = new Dictionary<int, ChessFigureOnPositionInfo>();
            }
        }

        public ChessGameProgressInfo ChessGameProgressInfo { get; set; }

        public ChessColors PlayerOnTurn { get; set; }

        public Dictionary<char, Dictionary<int, ChessFigureOnPositionInfo>> board;
    }
}