namespace ChessGameLogic
{
    using System;

    using ChessGameLogic.ChessFigures;
    using ChessGameLogic.ChessFigures.Interfaces;
    using ChessGameLogic.ChessMoves;
    using ChessGameLogic.Enums;
    using ChessGameLogic.Exceptions;

    internal class ChessBoard
    {
        private readonly IFigure[,] board;

        internal ChessBoard()
        {
            this.board = new IFigure[8, 8];
            this.InitializeBoard();
        }

        internal void RemoveFigureOnPosition(ChessBoardPosition positionOnTheBoard)
        {
            this.board[8 - positionOnTheBoard.Vertical, positionOnTheBoard.Horizontal - 'a'] = null;
        }

        internal IFigure GetFigureOnPosition(ChessBoardPosition positionOnTheBoard)
        {
            return this.board[8 - positionOnTheBoard.Vertical, positionOnTheBoard.Horizontal - 'a'];
        }

        internal void PutFigureOnPosition(ChessBoardPosition positionOnTheBoard, IFigure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            this.board[8 - positionOnTheBoard.Vertical, positionOnTheBoard.Horizontal - 'a'] = figure;

            if (figure is ICastleableFigure)
            {
                ((ICastleableFigure)figure).Move();
            }
        }

        internal void PutFigureOnPositionWithoutMovingItActualy(ChessBoardPosition positionOnTheBoard, IFigure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            this.board[8 - positionOnTheBoard.Vertical, positionOnTheBoard.Horizontal - 'a'] = figure;
        }

        internal ChessBoard GetVirtualChessBoardAfterMove(NormalChessMovePositions normalMove)
        {
            ChessBoard chessBoard = this.CopyCurrentChessBoard();
            var figure = chessBoard.GetFigureOnPosition(normalMove.InitialPosition);
            chessBoard.RemoveFigureOnPosition(normalMove.InitialPosition);
            chessBoard.PutFigureOnPositionWithoutMovingItActualy(normalMove.TargetPosition, figure);

            return chessBoard;
        }

        internal ChessBoard(IFigure[,] figures)
        {
            if (figures.GetLength(0) != 8 && figures.GetLength(1) != 8)
            {
                throw new InvalidChessBoardException("Invalid chess board!");
            }

            this.board = figures;
        }

        private void InitializeBoard()
        {
            this.InitializeRooks();
            this.InitializeKnights();
            this.InializeBishops();

            this.InitializeQueens();
            this.InitializeKings();

            this.InitializePawns();
        }

        private void InitializePawns()
        {
            for (int i = 0; i <= 7; i++)
            {
                this.board[1, i] = new Pawn(ChessColors.Black);
            }

            for (int i = 0; i <= 7; i++)
            {
                this.board[6, i] = new Pawn(ChessColors.White);
            }
        }

        private void InitializeQueens()
        {
            this.board[0, 3] = new Queen(ChessColors.Black);
            this.board[7, 3] = new Queen(ChessColors.White);
        }

        private void InitializeKings()
        {
            this.board[0, 4] = new King(ChessColors.Black);
            this.board[7, 4] = new King(ChessColors.White);
        }

        private void InializeBishops()
        {
            this.board[0, 2] = new Bishop(ChessColors.Black);
            this.board[7, 5] = new Bishop(ChessColors.White);
            this.board[0, 5] = new Bishop(ChessColors.Black);
            this.board[7, 2] = new Bishop(ChessColors.White);
        }

        private void InitializeKnights()
        {
            this.board[0, 1] = new Knight(ChessColors.Black);
            this.board[7, 6] = new Knight(ChessColors.White);
            this.board[0, 6] = new Knight(ChessColors.Black);
            this.board[7, 1] = new Knight(ChessColors.White);
        }

        private void InitializeRooks()
        {
            this.board[0, 0] = new Rook(ChessColors.Black);
            this.board[7, 7] = new Rook(ChessColors.White);
            this.board[0, 7] = new Rook(ChessColors.Black);
            this.board[7, 0] = new Rook(ChessColors.White);
        }


        private ChessBoard CopyCurrentChessBoard()
        {
            IFigure[,] newFigures = new IFigure[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    newFigures[i, j] = this.board[i, j];
                }
            }

            ChessBoard chessBoard = new ChessBoard(newFigures);

            return chessBoard;
        }
    }
}
