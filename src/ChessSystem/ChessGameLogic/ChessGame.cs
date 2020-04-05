namespace ChessGameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ChessGameLogic.ChessFigures;
    using ChessGameLogic.ChessFigures.Interfaces;
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;

    /// <summary>
    /// The main class that performs operations with figure moves.
    /// Each instance is a completely new game.
    /// </summary>
    public class ChessGame
    {
        private readonly Func<ChessFigureProductionType> chooseFigureToProduceFunction;
        private readonly Action<EndGameResult> endGameHandleFunction;

        private ChessGameProgressInfo progressInfo;
        private ChessBoard chessBoard;
        private ChessColors playerOnTurn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        /// <param name="chooseFigureToProduceFunction">Function which will be called when the chess game needs to know what
        /// figure to produce in the place of pawn that have reached the end of the board.</param>
        /// <param name="endGameHandleFunction">Function which will be called when the chess game finishes in some way.
        /// The result can be found in the EndGameResult parameter.</param>
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
            this.progressInfo = ChessGameProgressInfo.InProgressWithoutCheck;

            this.chooseFigureToProduceFunction = chooseFigureToProduceFunction;
            this.endGameHandleFunction = endGameHandleFunction;
        }

        /// <summary>
        /// Gets the player who is on turn.
        /// </summary>
        public ChessColors PlayerOnTurn => this.playerOnTurn;

        /// <summary>
        /// Gets information about the current state of the game.
        /// </summary>
        public ChessGameProgressInfo GameProgressInfo => this.progressInfo;

        /// <summary>
        /// Performs normal chess move on the current chess game.
        /// </summary>
        /// <param name="initialPositionHorizontal">The horizontal dimension of the position of the chess board from which the figure starts moving - letter from 'a' to 'h'.</param>
        /// <param name="initialPositionVertical">The vertical dimension of the position of the chess board from which the figure starts moving - number from 1 to 8.</param>
        /// <param name="targetPositionHorizontal">The horizontal dimension of the position of the chess board to which the figure is supposed to go - letter from 'a' to 'h'.</param>
        /// <param name="targetPositionVertical">The vertical dimension of the position of the chess board to which the figure is supposed to go - number from 1 to 8.</param>
        /// <param name="figureType">The type of the figure.</param>
        /// <param name="color">The color of the figure.</param>
        /// <returns>NormalChessMoveValidationResult - contains the validation result of the move.</returns>
        public NormalChessMoveValidationResult NormalMove(
            char initialPositionHorizontal,
            int initialPositionVertical,
            char targetPositionHorizontal,
            int targetPositionVertical,
            ChessFigureType figureType,
            ChessColors color)
        {
            if (this.progressInfo == ChessGameProgressInfo.WhiteHaveWon
                || this.progressInfo == ChessGameProgressInfo.BlackHaveWon
                || this.progressInfo == ChessGameProgressInfo.GameHasEndedDraw)
            {
                return NormalChessMoveValidationResult.GameHasEnded;
            }

            if (color != this.playerOnTurn)
            {
                return NormalChessMoveValidationResult.PlayerIsNotOnTurn;
            }

            NormalChessMovePositions move = new NormalChessMovePositions(
                initialPositionHorizontal,
                initialPositionVertical,
                targetPositionHorizontal,
                targetPositionVertical);

            IFigure figure = this.chessBoard.GetFigureOnPosition(move.InitialPosition);

            Type actualFigureType = this.ConvertFromFigureTypeEnumToActualType(figureType);

            NormalChessMoveValidationResult validationResult = this.ValidateMove(move, actualFigureType, color);

            if (validationResult == NormalChessMoveValidationResult.ValidMove)
            {
                this.chessBoard.RemoveFigureOnPosition(move.InitialPosition);
                this.chessBoard.PutFigureOnPosition(move.TargetPosition, figure);

                if (figure is Pawn && ((Pawn)figure).isPositionProducable(move.TargetPosition))
                {
                    ChessFigureProductionType chessFigureProductionType = this.chooseFigureToProduceFunction();

                    Type figureToProduceType = this.ConvertFromChessFigureProductionTypeEnumToActualType(chessFigureProductionType);

                    this.ProducePawn(move.TargetPosition, figureToProduceType, color);
                }

                if (this.CheckForCheck(this.chessBoard, ChessColors.White))
                {
                    this.progressInfo = ChessGameProgressInfo.WhiteAreUnderCheck;
                }
                else if (this.CheckForCheck(this.chessBoard, ChessColors.Black))
                {
                    this.progressInfo = ChessGameProgressInfo.BlackAreUnderCheck;
                }
                else
                {
                    this.progressInfo = ChessGameProgressInfo.InProgressWithoutCheck;
                }

                if (this.CheckForDraw())
                {
                    this.endGameHandleFunction(EndGameResult.Draw);
                    this.progressInfo = ChessGameProgressInfo.GameHasEndedDraw;
                }

                if (this.CheckForMate(ChessColors.Black))
                {
                    this.endGameHandleFunction(EndGameResult.BlackWin);
                    this.progressInfo = ChessGameProgressInfo.BlackHaveWon;
                }

                if (this.CheckForMate(ChessColors.White))
                {
                    this.endGameHandleFunction(EndGameResult.WhiteWin);
                    this.progressInfo = ChessGameProgressInfo.WhiteHaveWon;
                }

                this.ChangePlayer();
            }

            return validationResult;
        }

        /// <summary>
        /// Method that gets all possible positions of placing the given figure.
        /// </summary>
        /// <param name="horizontal">The horizontal dimension of the chess board - letter from 'a' to 'h'.</param>
        /// <param name="vertical">The vertical dimension of the chess board - number from 1 to 8.</param>
        /// <param name="figureType">The type of the figure to move.</param>
        /// <param name="chessFigureColor">The color of the figure to move.</param>
        /// <returns>List of all possible positions.</returns>
        public List<Position> GetAllPossiblePositionsOfPlacingTheFigure(
            char horizontal,
            int vertical,
            ChessFigureType figureType,
            ChessColors chessFigureColor)
        {
            Type actualFigureType = this.ConvertFromFigureTypeEnumToActualType(figureType);

            ChessBoardPosition chessBoardPosition = new ChessBoardPosition(horizontal, vertical);

            List<Position> attackingPos = new List<Position>();

            for (char horizontalIterator = 'a'; horizontalIterator <= 'h'; horizontalIterator++)
            {
                for (int verticalIterator = 1; verticalIterator <= 8; verticalIterator++)
                {
                    NormalChessMovePositions move = new NormalChessMovePositions(
                        chessBoardPosition.Horizontal,
                        chessBoardPosition.Vertical,
                        horizontalIterator,
                        verticalIterator);

                    NormalChessMoveValidationResult validationResult = this.ValidateMove(
                        move,
                        actualFigureType,
                        chessFigureColor);

                    if (validationResult == NormalChessMoveValidationResult.ValidMove)
                    {
                        attackingPos.Add(new Position(horizontalIterator, verticalIterator));
                    }
                }
            }

            if (actualFigureType.FullName == typeof(King).FullName)
            {
                attackingPos.AddRange(this.GetAllPossiblePositionsOfRookWhenCastlingTheKing(chessBoardPosition, chessFigureColor)
                    .Select(position => new Position(position.Horizontal,position.Vertical)));
            }

            if (actualFigureType.FullName == typeof(Rook).FullName)
            {
                ChessBoardPosition kingPos = this.GetPossiblePositionOfKingWhenCastlingTheRook(
                    chessBoardPosition,
                    chessFigureColor);

                if (kingPos != null)
                {
                    attackingPos.Add(new Position(kingPos.Horizontal, kingPos.Vertical));
                }
            }

            return attackingPos;
        }

        /// <summary>
        /// Performs castling move over the current chess game.
        /// </summary>
        /// <param name="kingPositionHorizontal">The horizontal dimension of the king position of the chess board - letter from 'a' to 'h'.</param>
        /// <param name="kingPositionVertical">The vertical dimension of the king position of the chess board - number from 1 to 8.</param>
        /// <param name="rookPositionHorizontal">The horizontal dimension of the rook position of the chess board - letter from 'a' to 'h'.</param>
        /// <param name="rookPositionVertical">The vertical dimension of the rook position of the chess board - number from 1 to 8.</param>
        /// <param name="colorOfTheFigures">The color of the figures.</param>
        /// <returns>CastlingMoveValidationResult containing the validation result of the castling operation.</returns>
        public CastlingMoveValidationResult MakeCastling(
            char kingPositionHorizontal,
            int kingPositionVertical,
            char rookPositionHorizontal,
            int rookPositionVertical,
            ChessColors colorOfTheFigures)
        {
            if (this.progressInfo == ChessGameProgressInfo.WhiteHaveWon
                || this.progressInfo == ChessGameProgressInfo.BlackHaveWon
                || this.progressInfo == ChessGameProgressInfo.GameHasEndedDraw)
            {
                return CastlingMoveValidationResult.GameHasEnded;
            }

            if (colorOfTheFigures != this.playerOnTurn)
            {
                return CastlingMoveValidationResult.PlayerIsNotOnTurn;
            }

            ChessBoardPosition kingPosition = new ChessBoardPosition(kingPositionHorizontal, kingPositionVertical);
            ChessBoardPosition rookPosition = new ChessBoardPosition(rookPositionHorizontal, rookPositionVertical);

            CastlingMoveValidationResult castlingMoveValidationResult = this.IsValidCastling(kingPosition, rookPosition, colorOfTheFigures);

            if (castlingMoveValidationResult != CastlingMoveValidationResult.ValidCastling)
            {
                return castlingMoveValidationResult;
            }

            IFigure kingFigure = this.chessBoard.GetFigureOnPosition(kingPosition);
            IFigure rookFigure = this.chessBoard.GetFigureOnPosition(rookPosition);

            this.chessBoard.RemoveFigureOnPosition(kingPosition);
            this.chessBoard.RemoveFigureOnPosition(rookPosition);

            if (colorOfTheFigures == ChessColors.White)
            {
                if (rookPosition.Horizontal == 'a')
                {
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('c', 1), kingFigure);
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('d', 1), rookFigure);
                }
                else if (rookPosition.Horizontal == 'h')
                {
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('g', 1), kingFigure);
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('f', 1), rookFigure);
                }
            }
            else if (colorOfTheFigures == ChessColors.Black)
            {
                if (rookPosition.Horizontal == 'a')
                {
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('c', 8), kingFigure);
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('d', 8), rookFigure);
                }
                else if (rookPosition.Horizontal == 'h')
                {
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('g', 8), kingFigure);
                    this.chessBoard.PutFigureOnPosition(new ChessBoardPosition('f', 8), rookFigure);
                }
            }

            this.ChangePlayer();

            return castlingMoveValidationResult;
        }

        private List<ChessBoardPosition> GetAllPossiblePositionsOfRookWhenCastlingTheKing(
             ChessBoardPosition kingPosition,
             ChessColors kingFigureColor)
        {
            List<ChessBoardPosition> rookPositions = new List<ChessBoardPosition>();

            if (this.IsValidCastling(kingPosition, new ChessBoardPosition('a', 1), kingFigureColor)
                == CastlingMoveValidationResult.ValidCastling)
            {
                rookPositions.Add(new ChessBoardPosition('a', 1));
            }

            if (this.IsValidCastling(kingPosition, new ChessBoardPosition('h', 1), kingFigureColor)
                == CastlingMoveValidationResult.ValidCastling)
            {
                rookPositions.Add(new ChessBoardPosition('h', 1));
            }

            if (this.IsValidCastling(kingPosition, new ChessBoardPosition('a', 8), kingFigureColor)
                == CastlingMoveValidationResult.ValidCastling)
            {
                rookPositions.Add(new ChessBoardPosition('a', 8));
            }

            if (this.IsValidCastling(kingPosition, new ChessBoardPosition('h', 8), kingFigureColor)
                == CastlingMoveValidationResult.ValidCastling)
            {
                rookPositions.Add(new ChessBoardPosition('h', 8));
            }

            return rookPositions;
        }

        private ChessBoardPosition GetPossiblePositionOfKingWhenCastlingTheRook(
            ChessBoardPosition rookPosition,
            ChessColors rookFigureColor)
        {
            if (rookFigureColor == ChessColors.White)
            {
                if (this.IsValidCastling(new ChessBoardPosition('e', 1), rookPosition, rookFigureColor)
                    == CastlingMoveValidationResult.ValidCastling)
                {
                    return new ChessBoardPosition('e', 1);
                }
            }
            else if (rookFigureColor == ChessColors.Black)
            {
                if (this.IsValidCastling(new ChessBoardPosition('e', 8), rookPosition, rookFigureColor)
                    == CastlingMoveValidationResult.ValidCastling)
                {
                    return new ChessBoardPosition('e', 8);
                }
            }

            return null;
        }

        private CastlingMoveValidationResult IsValidCastling(
            ChessBoardPosition kingPosition,
            ChessBoardPosition rookPosition,
            ChessColors colorOfTheFigures)
        {
            IFigure kingFigure = this.chessBoard.GetFigureOnPosition(kingPosition);
            IFigure rookFigure = this.chessBoard.GetFigureOnPosition(rookPosition);
            Type actualKingFigureType = kingFigure?.GetType();
            Type actualRookFigureType = rookFigure?.GetType();

            if (kingFigure == null || actualKingFigureType.FullName != typeof(King).FullName || kingFigure.Color != colorOfTheFigures)
            {
                return CastlingMoveValidationResult.KingWithGivenColorNotFountOntheGivenPoition;
            }

            if (rookFigure == null || actualRookFigureType.FullName != typeof(Rook).FullName || rookFigure.Color != colorOfTheFigures)
            {
                return CastlingMoveValidationResult.RookWithGivenColorNotFountOntheGivenPoition;
            }

            if (((King)kingFigure).HasBeenMovedFromTheStartOfTheGame || ((Rook)rookFigure).HasBeenMovedFromTheStartOfTheGame)
            {
                return CastlingMoveValidationResult.FiguresHaveBeenMovedFromTheStartOfTheGame;
            }

            if (this.CheckForCheck(this.chessBoard, colorOfTheFigures))
            {
                return CastlingMoveValidationResult.TheKingThatHasToMakeTheCastlingIsUnderCheck;
            }

            List<ChessBoardPosition> positionOnTheBoardBetweenRookAndKingAndRookPosition = ((Rook)rookFigure)
                .GetPositionsInTheWayOfMove(new NormalChessMovePositions(
                    rookPosition.Horizontal,
                    rookPosition.Vertical,
                    kingPosition.Horizontal,
                    kingPosition.Vertical))
                .ToList();

            positionOnTheBoardBetweenRookAndKingAndRookPosition.Add(rookPosition);

            foreach (var pos in positionOnTheBoardBetweenRookAndKingAndRookPosition)
            {
                if (pos.Equals(rookPosition) == false)
                {
                    if (this.chessBoard.GetFigureOnPosition(pos) != null)
                    {
                        return CastlingMoveValidationResult.ThereIsFigureBetweenTheRookAndTheKing;
                    }
                }

                var virtualBoard = this.chessBoard.GetVirtualChessBoardAfterMove(new NormalChessMovePositions(
                    kingPosition.Horizontal,
                    kingPosition.Vertical,
                    pos.Horizontal,
                    pos.Vertical));

                if (this.CheckForCheck(virtualBoard, colorOfTheFigures))
                {
                    return CastlingMoveValidationResult.SomeOfTheFieldsBetweenTheRookAndTheKingAreUnderCheck;
                }
            }

            return CastlingMoveValidationResult.ValidCastling;
        }

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

        private NormalChessMoveValidationResult ValidateMove(NormalChessMovePositions positions, Type figureType, ChessColors color)
        {
            IFigure figureToMove = this.chessBoard.GetFigureOnPosition(positions.InitialPosition);
            Type actualFigureType = figureToMove.GetType();
            IFigure figureOnTargetPosition = this.chessBoard.GetFigureOnPosition(positions.TargetPosition);
            if (figureToMove == null || actualFigureType.FullName != figureType.FullName || figureToMove.Color != color)
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
                        (((Pawn)figureToMove).AreMovePositionsPossible(positions) == false &&
                    ((Pawn)figureToMove).IsAttackingMovePossible(positions) == false) ||
                            (
                                ((Pawn)figureToMove).IsAttackingMovePossible(positions) &&
                                (figureOnTargetPosition == null
                                || figureOnTargetPosition.Color == figureToMove.Color)))
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

            if (this.CheckForCheck(chessBoard, color))
            {
                return NormalChessMoveValidationResult.MovementResultsInCheckOfTheFriendlyKing;
            }

            return NormalChessMoveValidationResult.ValidMove;
        }

        private bool CheckForCheck(ChessBoard chessBoard, ChessColors defensiveColor)
        {
            for (char horizontal = 'a'; horizontal <= 'h'; horizontal++)
            {
                for (int vertical = 1; vertical <= 8; vertical++)
                {
                    var figure = chessBoard.GetFigureOnPosition(new ChessBoardPosition(horizontal, vertical));
                    if (figure == null || figure.Color == defensiveColor)
                    {
                        continue;
                    }

                    List<ChessBoardPosition> positionsAttacked =
                        this.PossiblePositionsMovement(
                            figure.GetType(),
                            new ChessBoardPosition(horizontal, vertical),
                            this.GetOppositeColor(defensiveColor),
                            chessBoard)
                        ;

                    foreach (var position in positionsAttacked)
                    {
                        IFigure figureOnPosition = chessBoard.GetFigureOnPosition(position);
                        if (figureOnPosition is King && figureOnPosition.Color == defensiveColor)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckForMate(ChessColors attackingColor)
        {
            ChessColors defensiveColor = this.GetOppositeColor(attackingColor);
            return this.CheckIfPlayerHasNoValidMove(defensiveColor) && this.CheckForCheck(this.chessBoard, defensiveColor);
        }

        private bool CheckForDraw()
        {
            if (this.CheckIfPlayerHasNoValidMove(ChessColors.Black) && (this.CheckForCheck(this.chessBoard, ChessColors.Black) == false))
            {
                return true;
            }

            if (this.CheckIfPlayerHasNoValidMove(ChessColors.White) && (this.CheckForCheck(this.chessBoard, ChessColors.White) == false))
            {
                return true;
            }

            return false;
        }

        private bool CheckIfPlayerHasNoValidMove(ChessColors color)
        {
            for (char i = 'a'; i <= 'h'; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    IFigure figureFromDefensiveSide = this.chessBoard.GetFigureOnPosition(new ChessBoardPosition(i, j));

                    if (figureFromDefensiveSide == null || figureFromDefensiveSide.Color == this.GetOppositeColor(color))
                    {
                        continue;
                    }

                    for (char horizontal = 'a'; horizontal <= 'h'; horizontal++)
                    {
                        for (int vertical = 1; vertical <= 8; vertical++)
                        {
                            if (this.ValidateMove(
                                new NormalChessMovePositions(i, j, horizontal, vertical),
                                figureFromDefensiveSide.GetType(),
                                color) == NormalChessMoveValidationResult.ValidMove)
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
                    if (this.ValidateMoveWithoutCheck(
                        new NormalChessMovePositions(position.Horizontal, position.Vertical, horizontal, vertical),
                        figure,
                        colors,
                        chessBoardParam) == NormalChessMoveValidationResult.ValidMove)
                    {
                        positions.Add(new ChessBoardPosition(horizontal, vertical));
                    }
                }
            }

            return positions;
        }

        private NormalChessMoveValidationResult ValidateMoveWithoutCheck(NormalChessMovePositions positions, Type figureType, ChessColors color, ChessBoard chessBoardParam)
        {
            IFigure figureToMove = chessBoardParam.GetFigureOnPosition(positions.InitialPosition);
            IFigure figureOnTargetPosition = chessBoardParam.GetFigureOnPosition(positions.TargetPosition);
            if (figureToMove == null || figureToMove.GetType() != figureType || figureToMove.Color != color)
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
                        (((Pawn)figureToMove).AreMovePositionsPossible(positions) == false &&
                    ((Pawn)figureToMove).IsAttackingMovePossible(positions) == false) ||
                            (
                                ((Pawn)figureToMove).IsAttackingMovePossible(positions) &&
                                (figureOnTargetPosition == null
                                || figureOnTargetPosition.Color == figureToMove.Color)))
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

        private void ProducePawn(ChessBoardPosition positionOnTheBoard, Type producedFigureType, ChessColors color)
        {
            IFigure figure = (IFigure)Activator.CreateInstance(producedFigureType, color);

            if (figure is ICastleableFigure)
            {
                ((ICastleableFigure)figure).Move();
            }

            this.chessBoard.PutFigureOnPosition(positionOnTheBoard, figure);
        }

        private Type ConvertFromFigureTypeEnumToActualType(ChessFigureType chessFigureType)
        {
            return this.GetType().Assembly.GetType($"ChessGameLogic.ChessFigures.{chessFigureType}");
        }

        private Type ConvertFromChessFigureProductionTypeEnumToActualType(ChessFigureProductionType chessFigureType)
        {
            return this.GetType().Assembly.GetType($"ChessGameLogic.ChessFigures.{chessFigureType}");
        }
    }
}
