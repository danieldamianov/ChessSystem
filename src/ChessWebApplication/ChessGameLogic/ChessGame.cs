using ChessGameLogic.ChessFigures;
using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.ClientInteractionEntities;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessGameLogic
{
    public class ChessGame
    {
        private ChessBoard chessBoard;
        private ChessColors playerOnTurn;

        private ChessColors GetOppositeColor(ChessColors color)
        {
            if (color == ChessColors.Black)
            {
                return ChessColors.White;
            }
            return ChessColors.Black;
        }

        private void ChangePlayer()
        {
            this.playerOnTurn = this.GetOppositeColor(this.playerOnTurn);
        }

        private NormalChessMoveValidationResult ValidateMove(NormalChessMovePositions positions, Type FigureType, ChessColors color)
        {
            IFigure figureToMove = this.chessBoard.GetFigureOnPosition(positions.InitialPosition);
            Type actualFigureType = figureToMove.GetType();
            IFigure figureOnTargetPosition = this.chessBoard.GetFigureOnPosition(positions.TargetPosition);
            if (figureToMove == null || actualFigureType.FullName != FigureType.FullName || figureToMove.Color != color)
            {
                return NormalChessMoveValidationResult.ThereIsntSuchFigureAndColorOnTheGivenPosition;
            }
            if (figureOnTargetPosition != null &&
                (figureOnTargetPosition.GetType() == typeof(King) || figureOnTargetPosition.Color == figureToMove.Color))
            {
                return NormalChessMoveValidationResult.TheFigureOnTheTargetPositionIsFriendlyOrEnemyKing;
            }

            if (figureToMove is Pawn)
            {

                if (((Pawn)figureToMove).AreMovePositionsPossible(positions) == true
                    && figureOnTargetPosition != null)
                {
                    return NormalChessMoveValidationResult.MovePositionsAreNotValid;
                }

                if (
                        ((((Pawn)figureToMove).AreMovePositionsPossible(positions) == false &&
                    ((Pawn)figureToMove).IsAttackingMovePossible(positions) == false) ||
                            (
                                ((Pawn)figureToMove).IsAttackingMovePossible(positions) &&
                                (figureOnTargetPosition == null
                                || figureOnTargetPosition.Color == figureToMove.Color)
                            )
                       ))
                {
                    return NormalChessMoveValidationResult.MovePositionsAreNotValid;
                }
            }
            else
            {
                if (figureToMove.AreMovePositionsPossible(positions) == false)
                {
                    return NormalChessMoveValidationResult.MovePositionsAreNotValid;
                }
            }

            if (figureToMove is IUnableToJumpFigure)
            {
                foreach (var position in ((IUnableToJumpFigure)figureToMove).GetPositionsInTheWayOfMove(positions))
                {
                    if (this.chessBoard.GetFigureOnPosition(position) != null)
                    {
                        return NormalChessMoveValidationResult.ThereAreOtherPiecesOnTheWay;
                    }
                }
            }

            ChessBoard chessBoard = this.chessBoard.GetVirtualChessBoardAfterMove(positions);

            if (CheckForCheck(chessBoard, color))
            {
                return NormalChessMoveValidationResult.MovementResultsInCheckOfTheFriendlyKing;
            }

            return NormalChessMoveValidationResult.ValidMove;

        }

        private bool CheckForCheck(ChessBoard chessBoard, ChessColors color)
        {
            for (char horizontal = 'a'; horizontal <= 'h'; horizontal++)
            {
                for (int vertical = 1; vertical <= 8; vertical++)
                {
                    var figure = chessBoard.GetFigureOnPosition(new ChessBoardPosition(horizontal, vertical));
                    if (figure == null || figure.Color == color)
                    {
                        continue;
                    }

                    List<ChessBoardPosition> positionsAttacked =
                        this.PossiblePositionsMovement(figure.GetType(), new ChessBoardPosition(horizontal, vertical), GetOppositeColor(color)
                        , chessBoard)
                        ;

                    foreach (var position in positionsAttacked)
                    {
                        IFigure gifureOnPosition = chessBoard.GetFigureOnPosition(position);
                        if (gifureOnPosition is King && gifureOnPosition.Color == color)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private List<ChessBoardPosition> PossiblePositionsMovement(Type type, ChessBoardPosition chessBoardPosition, ChessColors chessColors, ChessBoard chessBoard)
        {
            throw new NotImplementedException();
        }

        public ChessGame()
        {
            this.playerOnTurn = ChessColors.White;
            this.chessBoard = new ChessBoard();
        }
        public ChessColors PlayerOnTurn => this.playerOnTurn;

    }
}
