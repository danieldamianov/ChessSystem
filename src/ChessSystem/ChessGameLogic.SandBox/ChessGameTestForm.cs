namespace ChessGameLogic.SandBox
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using ChessGameLogic.ClientInteractionEntities;
    using ChessGameLogic.Enums;
    using ChessGameLogic.SandBox.Properties;


    /// <summary>
    /// Test form for a chess game.
    /// </summary>
    public partial class ChessGameTestForm : Form
    {
        private ChessGame chessGame;
        private ChessField chessFieldSelected;
        private Button[,] board;
        private FlowLayoutPanel boardPanel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGameTestForm"/> class.
        /// </summary>
        public ChessGameTestForm()
        {
            this.boardPanel = new FlowLayoutPanel();
            this.Controls.Add(this.boardPanel);
            this.InitializeComponent();
            this.ConfigureBoardPanel();
            this.chessGame = new ChessGame(this.ChooseFigureToProduce, this.HandleGameEnd);
            this.InitialzeBoard(this.chessGame);

            this.Location = new Point(0, 0);
            this.Size = new Size(600, 600);
            this.ClientSize = new Size(600, 600);
        }

        private void HandleGameEnd(EndGameResult endGameResult)
        {
            switch (endGameResult)
            {
                case EndGameResult.BlackWin:
                    MessageBox.Show("Black win");
                    break;
                case EndGameResult.WhiteWin:
                    MessageBox.Show("White win");
                    break;
                case EndGameResult.Draw:
                    MessageBox.Show("Draw win");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private ChessFigureProductionType ChooseFigureToProduce()
        {
            ChessFigureProductionType figureChosen;
            DialogResult dialogResult = MessageBox.Show("Do you want to get a QUEEN?", string.Empty, MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                figureChosen = ChessFigureProductionType.Queen;
            }
            else
            {
                DialogResult dialogResult2 = MessageBox.Show("Do you want to get a ROOK?", string.Empty, MessageBoxButtons.YesNo);
                if (dialogResult2 == DialogResult.Yes)
                {
                    figureChosen = ChessFigureProductionType.Rook;
                }
                else
                {

                    DialogResult dialogResult3 = MessageBox.Show("Do you want to get a BISHOP?", string.Empty, MessageBoxButtons.YesNo);
                    if (dialogResult3 == DialogResult.Yes)
                    {
                        figureChosen = ChessFigureProductionType.Bishop;
                    }
                    else
                    {
                        figureChosen = ChessFigureProductionType.Knight;
                    }
                }
            }

            return figureChosen;
        }

        private void ConfigureBoardPanel()
        {
            this.boardPanel.Location = new Point(0, 0);
            this.boardPanel.Margin = new Padding(0);
            this.boardPanel.Name = "BoardPanel";
            this.boardPanel.Size = new Size(600, 600);
            this.boardPanel.TabIndex = 3;

            this.boardPanel.Visible = true;

            this.board = new ChessField[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j] = new ChessField();
                    this.board[i, j].Click += new EventHandler(this.MoveHandler);
                    ((ChessField)this.board[i, j]).positionOnTheBoard = new ChessPosition(Convert.ToChar(j + 'a'), 8 - i);
                    this.board[i, j].FlatStyle = FlatStyle.Flat;
                    this.board[i, j].FlatAppearance.BorderSize = 0;
                    this.board[i, j].Size = new System.Drawing.Size(70, 70);
                    this.board[i, j].Margin = new Padding(0);
                    this.boardPanel.Controls.Add(this.board[i, j]);
                }
            }
        }

        private void MoveHandler(object sender, EventArgs eventArgs)
        {
            if (this.chessFieldSelected == null)
            {
                this.chessFieldSelected = (ChessField)sender;

                List<Position> attackingPos =
                    this.chessGame.GetAllPossiblePositionsOfPlacingTheFigure(
                        this.chessFieldSelected.positionOnTheBoard.Horizontal,
                        this.chessFieldSelected.positionOnTheBoard.Vertical,
                        (ChessFigureType)this.chessFieldSelected.chessFigure,
                        (ChessColors)this.chessFieldSelected.chessFigureColor);

                foreach (var field in this.board)
                {
                    if (((ChessField)field).positionOnTheBoard.Equals(this.chessFieldSelected.positionOnTheBoard) == false
                        && attackingPos.Any(ap => ((ChessField)field).positionOnTheBoard.Equals(ap)) == false)
                    {
                        field.Enabled = false;
                    }
                    else
                    {
                        field.Enabled = true;
                    }
                }
            }
            else
            {
                var targetField = (ChessField)sender;

                var figureOnInitialPosition = this.chessGame.GetFihureOnPositionInfo(
                    this.chessFieldSelected.positionOnTheBoard.Horizontal,
                    this.chessFieldSelected.positionOnTheBoard.Vertical);

                var figureOnTargetPosition = this.chessGame.GetFihureOnPositionInfo(
                    targetField.positionOnTheBoard.Horizontal,
                    targetField.positionOnTheBoard.Vertical);

                if (figureOnTargetPosition != null)
                {
                    if (figureOnInitialPosition.figureType == ChessFigureType.King
                        && figureOnTargetPosition.figureType == ChessFigureType.Bishop)
                    {
                        this.chessGame.MakeCastling(
                            this.chessFieldSelected.positionOnTheBoard.Horizontal,
                            this.chessFieldSelected.positionOnTheBoard.Vertical,
                            targetField.positionOnTheBoard.Horizontal,
                            targetField.positionOnTheBoard.Vertical,
                            figureOnInitialPosition.figureColor);
                    }

                    if (figureOnInitialPosition.figureType == ChessFigureType.Bishop
                        && figureOnTargetPosition.figureType == ChessFigureType.King)
                    {
                        this.chessGame.MakeCastling(
                            targetField.positionOnTheBoard.Horizontal,
                            targetField.positionOnTheBoard.Vertical,
                            this.chessFieldSelected.positionOnTheBoard.Horizontal,
                            this.chessFieldSelected.positionOnTheBoard.Vertical,
                            figureOnInitialPosition.figureColor);
                    }
                }


                this.chessGame.NormalMove(
                    this.chessFieldSelected.positionOnTheBoard.Horizontal,
                    this.chessFieldSelected.positionOnTheBoard.Vertical,
                    ((ChessField)sender).positionOnTheBoard.Horizontal,
                    ((ChessField)sender).positionOnTheBoard.Vertical,
                    (ChessFigureType)this.chessFieldSelected.chessFigure,
                    (ChessColors)this.chessFieldSelected.chessFigureColor);

                this.InitialzeBoard(this.chessGame);

                this.chessFieldSelected = null;
            }
        }

        private Color SwitchColor(Color color)
        {
            if (color == Color.DarkGray)
            {
                return Color.White;
            }

            return Color.DarkGray;
        }

        private void InitialzeBoard(ChessGame chessGame)
        {
            Color color = Color.DarkGray;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.board[i, j].BackColor = color;
                    ChessFigureOnPositionInfo chessFigureOnPositionInfo = chessGame.GetFihureOnPositionInfo(Convert.ToChar('a' + j), 8 - i);
                    if (chessFigureOnPositionInfo != null)
                    {
                        this.board[i, j].Image =
                            (Image)typeof(Resources)
                            .GetProperty($"{chessFigureOnPositionInfo.figureType}{chessFigureOnPositionInfo.figureColor}{color.Name}", BindingFlags.Static | BindingFlags.Public | System.Reflection.BindingFlags.NonPublic).GetValue(null); //= chessBoard.board[i, j].GetFigureSymbol().ToString(); 
                        ((ChessField)this.board[i, j]).chessFigure = chessFigureOnPositionInfo.figureType;
                        ((ChessField)this.board[i, j]).chessFigureColor = chessFigureOnPositionInfo.figureColor;
                    }
                    else
                    {
                        this.board[i, j].Image = null;
                        ((ChessField)this.board[i, j]).chessFigure = null;
                        ((ChessField)this.board[i, j]).chessFigureColor = null;
                    }

                    if (((ChessField)this.board[i, j]).chessFigureColor != this.chessGame.PlayerOnTurn)
                    {
                        this.board[i, j].Enabled = false;
                    }
                    else
                    {
                        this.board[i, j].Enabled = true;
                    }

                    if (j != 7)
                    {
                        color = this.SwitchColor(color);
                    }
                }
            }
        }
    }
}
