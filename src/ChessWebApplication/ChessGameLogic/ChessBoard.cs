using ChessGameLogic.ChessFigures;
using ChessGameLogic.ChessFigures.Interfaces;
using ChessGameLogic.Enums;
using ChessGameLogic.Exceptions;

namespace ChessGameLogic
{
    internal class ChessBoard
    {
        private readonly IFigure[,] board;

        internal ChessBoard()
        {
            this.board = new IFigure[8, 8];
            this.InitializeBoard();
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
            InitializeRooks();
            InitializeKnights();
            InializeBishops();

            InitializeQueens();
            InitializeKings();

            InitializePawns();
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
    }
}
