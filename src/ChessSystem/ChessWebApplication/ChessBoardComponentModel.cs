using ChessGameLogic.ClientInteractionEntities;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWebApplication
{
    public class ChessBoardComponentModel
    {
        public ChessBoardComponentModel()
        {
            board = new Dictionary<char, Dictionary<int, ChessFigureOnPositionInfo>>();

            for (char i = 'a'; i <= 'h'; i++)
            {
                board[i] = new Dictionary<int, ChessFigureOnPositionInfo>();
            }
        }

        public ChessGameProgressInfo ChessGameProgressInfo { get; set; }

        public ChessColors PlayerOnTurn { get; set; }

        public Dictionary<char, Dictionary<int, ChessFigureOnPositionInfo>> board;
    }
}