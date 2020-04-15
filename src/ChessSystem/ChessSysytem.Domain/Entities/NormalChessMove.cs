using ChessSystem.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessSystem.Domain.Entities
{
    public class NormalChessMove : BaseEntitiy<string>
    {
        public string InitialPositionId { get; set; }

        public ChessBoardPosition InitialPosition { get; set; }

        public string TargetPositionId { get; set; }

        public ChessBoardPosition TargetPosition { get; set; }

        public ChessFigureType ChessFigureType { get; set; }

        public ChessFigureColor ChessFigureColor { get; set; }
    }
}
