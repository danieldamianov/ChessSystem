namespace ChessGameLogic.ChessFigures
{
    using System.Collections.Generic;

    using ChessGameLogic.ChessFigures.Interfaces;
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.Enums;

    internal class Queen : Figure, IUnableToJumpFigure
    {
        internal Queen(ChessColors color) 
            : base(color)
        {
        }

        public override bool AreMovePositionsPossible(NormalChessMovePositions normalMove)
        {
            Rook rook = new Rook(ChessColors.Black);
            Bishop bishop = new Bishop(ChessColors.Black);

            return rook.AreMovePositionsPossible(normalMove) || bishop.AreMovePositionsPossible(normalMove);
        }

        public IEnumerable<ChessBoardPosition> GetPositionsInTheWayOfMove(NormalChessMovePositions normalMove)
        {
            if (this.AreMovePositionsPossible(normalMove) == false)
            {
                return null;
            }

            IEnumerable<ChessBoardPosition> positionsOnTheBoard = new List<ChessBoardPosition>();

            Rook rook = new Rook(ChessColors.Black);
            Bishop bishop = new Bishop(ChessColors.Black);

            if (rook.AreMovePositionsPossible(normalMove))
            {
                positionsOnTheBoard = rook.GetPositionsInTheWayOfMove(normalMove);
            }

            if (bishop.AreMovePositionsPossible(normalMove))
            {
                positionsOnTheBoard = bishop.GetPositionsInTheWayOfMove(normalMove);
            }

            return positionsOnTheBoard;
        }
    }
}
