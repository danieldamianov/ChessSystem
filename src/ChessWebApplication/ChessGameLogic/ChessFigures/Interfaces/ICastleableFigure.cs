using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic.ChessFigures.Interfaces
{
    internal interface ICastleableFigure
    {
        bool HasBeenMovedFromTheStartOfTheGame { get; }

        void Move();
    }
}
