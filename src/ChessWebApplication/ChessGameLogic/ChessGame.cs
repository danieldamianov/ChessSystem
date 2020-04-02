using ChessGameLogic.ChessFigures;
using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.ChessMoves;
using ChessGameLogic.ClientInteractionEntities;
using ChessGameLogic.Enums;
using System;
using System.Collections.Generic;

namespace ChessGameLogic
{
    public class ChessGame
    {
        private bool gameHasEnded;
        private ChessBoard chessBoard;
        private ChessColors playerOnTurn;

        private readonly Func<ChessFigureProductionType> ChooseFigureToProduceFunction; 
        private readonly Action<EndGameResult> EndGameHandleFunction;

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

        private bool CheckIfPlayerHasNoValidMove(ChessColors Color)
        {
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    IFigure figureFromDefensiveSide = this.chessBoard.GetFigureOnPosition(new ChessBoardPosition(i, j));

                    if (figureFromDefensiveSide == null || figureFromDefensiveSide.Color == this.GetOppositeColor(Color))
                    {
                        continue;
                    }

                    for (char horizontal = 'a'; horizontal <= 'h'; horizontal++)
                    {
                        for (int vertical = 1; vertical <= 8; vertical++)
                        {
                            if (this.ValidateMove(new NormalChessMovePositions(i, j
                                , horizontal, vertical),
                                figureFromDefensiveSide.GetType()
                        , Color) == NormalChessMoveValidationResult.ValidMove)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private List<ChessBoardPosition> PossiblePositionsMovement(Type figure, ChessBoardPosition position, ChessColors colors, ChessBoard chessBoardParam)
        {
            List<ChessBoardPosition> positions = new List<ChessBoardPosition>();
            for (char horizontal = 'a'; horizontal <= 'h'; horizontal++)
            {
                for (int vertical = 1; vertical <= 8; vertical++)
                {
                    if (this.ValidateMoveWithoutCheck(new NormalChessMovePositions(position.Horizontal, position.Vertical, horizontal, vertical)
                        , figure, colors, chessBoardParam) == NormalChessMoveValidationResult.ValidMove)
                    {
                        positions.Add(new ChessBoardPosition(horizontal, vertical));
                    }
                }
            }

            return positions;
        }

        private NormalChessMoveValidationResult ValidateMoveWithoutCheck(NormalChessMovePositions positions, Type FigureType, ChessColors color, ChessBoard chessBoardParam)
        {
            IFigure figureToMove = chessBoardParam.GetFigureOnPosition(positions.InitialPosition);
            IFigure figureOnTargetPosition = chessBoardParam.GetFigureOnPosition(positions.TargetPosition);
            if (figureToMove == null || figureToMove.GetType() != FigureType || figureToMove.Color != color)
            {
                return NormalChessMoveValidationResult.ThereIsntSuchFigureAndColorOnTheGivenPosition;
            }
            if (figureOnTargetPosition != null &&
                (figureOnTargetPosition.Color == figureToMove.Color))
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
                    if (chessBoardParam.GetFigureOnPosition(position) != null)
                    {
                        return NormalChessMoveValidationResult.ThereAreOtherPiecesOnTheWay;
                    }
                }
            }

            return NormalChessMoveValidationResult.ValidMove;
        }

        public ChessGame(Func<ChessFigureProductionType> chooseFigureToProduceFunction, Action<EndGameResult> endGameHandleFunction)
        {
            if (chooseFigureToProduceFunction == null)
            {
                throw new ArgumentNullException(nameof(chooseFigureToProduceFunction));
            }

            if (endGameHandleFunction == null)
            {
                throw new ArgumentNullException(nameof(endGameHandleFunction));
            }

            this.playerOnTurn = ChessColors.White;
            this.chessBoard = new ChessBoard();
            this.ChooseFigureToProduceFunction = chooseFigureToProduceFunction;
            this.EndGameHandleFunction = endGameHandleFunction;
        }
        public ChessColors PlayerOnTurn => this.playerOnTurn;
        public bool GameHasEnded => this.gameHasEnded;

    }
}
