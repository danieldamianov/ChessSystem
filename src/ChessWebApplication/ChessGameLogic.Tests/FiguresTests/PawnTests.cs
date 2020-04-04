namespace ChessGameLogic.Tests.FiguresTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using ChessGameLogic.Enums;
    using NUnit.Framework;

    /// <summary>
    /// Tests for the pawn class.
    /// </summary>
    [TestFixture]
    public class PawnTests
    {
        public PawnTests()
        {
            this.PawnType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.Pawn");

            var constructor = this.PawnType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .First();

            this.WhitePawnInstance = constructor
                .Invoke(new object[] { ChessColors.White });

            this.BlackPawnInstance = constructor
                .Invoke(new object[] { ChessColors.Black });
        }

        public Type PawnType { get; set; }

        public object BlackPawnInstance { get; set; }

        public object WhitePawnInstance { get; set; }

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColorOfWhiteInstance = this.PawnType.GetProperty("Color")
                .GetValue(this.WhitePawnInstance);

            var actualColorOfBlackInstance = this.PawnType.GetProperty("Color")
                .GetValue(this.BlackPawnInstance);

            Assert.That((ChessColors)actualColorOfWhiteInstance == ChessColors.White);
            Assert.That((ChessColors)actualColorOfBlackInstance == ChessColors.Black);
        }

        [Test]
        [TestCase('a', 2, 'a', 3, ChessColors.White, true)]
        [TestCase('d', 2, 'd', 4, ChessColors.White, true)]
        [TestCase('d', 5, 'd', 6, ChessColors.White, true)]

        [TestCase('d', 7, 'd', 6, ChessColors.Black, true)]
        [TestCase('f', 7, 'f', 5, ChessColors.Black, true)]
        [TestCase('c', 5, 'c', 4, ChessColors.Black, true)]

        [TestCase('d', 1, 'd', 2, ChessColors.White, false)]
        [TestCase('d', 1, 'd', 3, ChessColors.White, false)]

        [TestCase('f', 8, 'f', 7, ChessColors.Black, false)]
        [TestCase('f', 8, 'f', 6, ChessColors.Black, false)]

        [TestCase('a', 2, 'a', 3, ChessColors.Black, false)]
        [TestCase('d', 2, 'd', 4, ChessColors.Black, false)]
        [TestCase('d', 5, 'd', 6, ChessColors.Black, false)]

        [TestCase('d', 7, 'd', 6, ChessColors.White, false)]
        [TestCase('f', 7, 'f', 5, ChessColors.White, false)]
        [TestCase('c', 5, 'c', 4, ChessColors.White, false)]

        [TestCase('d', 1, 'd', 2, ChessColors.Black, false)]
        [TestCase('d', 1, 'd', 3, ChessColors.Black, false)]

        [TestCase('f', 8, 'f', 7, ChessColors.White, false)]
        [TestCase('f', 8, 'f', 6, ChessColors.White, false)]

        [TestCase('d', 5, 'd', 7, ChessColors.White, false)]
        [TestCase('f', 4, 'f', 2, ChessColors.Black, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, ChessColors pawnColor, bool isValid)
        {
            var arePositionsPossibleMethod = this.PawnType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            bool actualResult;

            if (pawnColor == ChessColors.White)
            {
                actualResult = (bool)arePositionsPossibleMethod.Invoke(this.WhitePawnInstance, new object[] { move });
            }
            else
            {
                actualResult = (bool)arePositionsPossibleMethod.Invoke(this.BlackPawnInstance, new object[] { move });
            }

            Assert.AreEqual(isValid, actualResult);
        }

        [Test]
        [TestCase('d', 1, 'c', 2, ChessColors.White, false)]
        [TestCase('d', 1, 'd', 2, ChessColors.White, false)]
        [TestCase('a', 2, 'a', 3, ChessColors.White, false)]
        [TestCase('a', 2, 'a', 4, ChessColors.White, false)]
        [TestCase('c', 3, 'c', 4, ChessColors.White, false)]
        [TestCase('c', 3, 'f', 7, ChessColors.White, false)]

        [TestCase('e', 8, 'd', 7, ChessColors.Black, false)]
        [TestCase('e', 8, 'e', 7, ChessColors.Black, false)]
        [TestCase('b', 7, 'b', 6, ChessColors.Black, false)]
        [TestCase('b', 7, 'b', 5, ChessColors.Black, false)]
        [TestCase('c', 3, 'c', 2, ChessColors.Black, false)]
        [TestCase('f', 7, 'c', 3, ChessColors.Black, false)]

        [TestCase('d', 2, 'c', 3, ChessColors.White, true)]
        [TestCase('d', 2, 'e', 3, ChessColors.White, true)]
        [TestCase('g', 5, 'f', 6, ChessColors.White, true)]

        [TestCase('d', 2, 'c', 3, ChessColors.Black, false)]
        [TestCase('d', 2, 'e', 3, ChessColors.Black, false)]
        [TestCase('g', 5, 'f', 6, ChessColors.Black, false)]

        [TestCase('d', 7, 'c', 6, ChessColors.Black, true)]
        [TestCase('d', 7, 'e', 6, ChessColors.Black, true)]
        [TestCase('c', 5, 'b', 4, ChessColors.Black, true)]

        [TestCase('d', 7, 'c', 6, ChessColors.White, false)]
        [TestCase('d', 7, 'e', 6, ChessColors.White, false)]
        [TestCase('c', 5, 'b', 4, ChessColors.White, false)]
        public void TestIsAttackingMovePossibleMethod(
            char initialHorizontal,
            int initialVertical,
            char targetHorizontal,
            int targetVertical,
            ChessColors pawnColor,
            bool isValid)
        {
            var isAttackingMovePossibleMethod = this.PawnType.GetMethod("IsAttackingMovePossible",
                BindingFlags.Instance | BindingFlags.NonPublic);

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            bool actualResult;

            if (pawnColor == ChessColors.White)
            {
                actualResult = (bool)isAttackingMovePossibleMethod.Invoke(this.WhitePawnInstance, new object[] { move });
            }
            else
            {
                actualResult = (bool)isAttackingMovePossibleMethod.Invoke(this.BlackPawnInstance, new object[] { move });
            }

            Assert.AreEqual(isValid, actualResult);
        }

        [Test]
        [TestCase('a', 2, 'a', 3, ChessColors.White, new object[] { })]
        [TestCase('d', 2, 'd', 4, ChessColors.White, new object[] { 'd', 3 })]
        [TestCase('d', 5, 'd', 6, ChessColors.White, new object[] { })]

        [TestCase('d', 7, 'd', 6, ChessColors.Black, new object[] { })]
        [TestCase('f', 7, 'f', 5, ChessColors.Black, new object[] { 'f', 6 })]
        [TestCase('c', 5, 'c', 4, ChessColors.Black, new object[] { })]

        [TestCase('d', 1, 'd', 2, ChessColors.White, null)]
        [TestCase('d', 1, 'd', 3, ChessColors.White, null)]

        [TestCase('f', 8, 'f', 7, ChessColors.Black, null)]
        [TestCase('f', 8, 'f', 6, ChessColors.Black, null)]

        [TestCase('a', 2, 'a', 3, ChessColors.Black, null)]
        [TestCase('d', 2, 'd', 4, ChessColors.Black, null)]
        [TestCase('d', 5, 'd', 6, ChessColors.Black, null)]

        [TestCase('d', 7, 'd', 6, ChessColors.White, null)]
        [TestCase('f', 7, 'f', 5, ChessColors.White, null)]
        [TestCase('c', 5, 'c', 4, ChessColors.White, null)]

        [TestCase('d', 1, 'd', 2, ChessColors.Black, null)]
        [TestCase('d', 1, 'd', 3, ChessColors.Black, null)]

        [TestCase('f', 8, 'f', 7, ChessColors.White, null)]
        [TestCase('f', 8, 'f', 6, ChessColors.White, null)]

        [TestCase('d', 5, 'd', 7, ChessColors.White, null)]
        [TestCase('f', 4, 'f', 2, ChessColors.Black, null)]



        [TestCase('d', 1, 'c', 2, ChessColors.White, null)]
        [TestCase('d', 1, 'd', 2, ChessColors.White, null)]
        [TestCase('a', 2, 'a', 3, ChessColors.White, new object[] { })]
        [TestCase('a', 2, 'a', 4, ChessColors.White, new object[] { 'a', 3 })]
        [TestCase('c', 3, 'c', 4, ChessColors.White, new object[] { })]
        [TestCase('c', 3, 'f', 7, ChessColors.White, null)]

        [TestCase('e', 8, 'd', 7, ChessColors.Black, null)]
        [TestCase('e', 8, 'e', 7, ChessColors.Black, null)]
        [TestCase('b', 7, 'b', 6, ChessColors.Black, new object[] { })]
        [TestCase('b', 7, 'b', 5, ChessColors.Black, new object[] { 'b', 6 })]
        [TestCase('c', 3, 'c', 2, ChessColors.Black, new object[] { })]
        [TestCase('f', 7, 'c', 3, ChessColors.Black, null)]

        [TestCase('d', 2, 'c', 3, ChessColors.White, new object[] { })]
        [TestCase('d', 2, 'e', 3, ChessColors.White, new object[] { })]
        [TestCase('g', 5, 'f', 6, ChessColors.White, new object[] { })]

        [TestCase('d', 2, 'c', 3, ChessColors.Black, null)]
        [TestCase('d', 2, 'e', 3, ChessColors.Black, null)]
        [TestCase('g', 5, 'f', 6, ChessColors.Black, null)]

        [TestCase('d', 7, 'c', 6, ChessColors.Black, new object[] { })]
        [TestCase('d', 7, 'e', 6, ChessColors.Black, new object[] { })]
        [TestCase('c', 5, 'b', 4, ChessColors.Black, new object[] { })]

        [TestCase('d', 7, 'c', 6, ChessColors.White, null)]
        [TestCase('d', 7, 'e', 6, ChessColors.White, null)]
        [TestCase('c', 5, 'b', 4, ChessColors.White, null)]
        public void TestGetPositionsInTheWayOfMoveMethod(
            char initialHorizontal,
            int initialVertical,
            char targetHorizontal,
            int targetVertical,
            ChessColors pawnColor,
            object[] positionsInTheWayOfMove
            )
        {
            List<Tuple<char, int>> positions = new List<Tuple<char, int>>();

            if (positionsInTheWayOfMove != null)
            {
                for (int i = 0; i < positionsInTheWayOfMove.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        positions.Add(new Tuple<char, int>((char)positionsInTheWayOfMove[i], (int)positionsInTheWayOfMove[i + 1]));
                    }
                }
            }

            var getPositionsInTheWayOfMoveMethod = this.PawnType.GetMethod("GetPositionsInTheWayOfMove");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });

            ICollection actualPositionsInTheWayOfMove = null;

            if (pawnColor == ChessColors.White)
            {
                actualPositionsInTheWayOfMove = (ICollection)getPositionsInTheWayOfMoveMethod.Invoke(this.WhitePawnInstance, new object[] { move });
            }
            else
            {
                actualPositionsInTheWayOfMove = (ICollection)getPositionsInTheWayOfMoveMethod.Invoke(this.BlackPawnInstance, new object[] { move });
            }

            if (positionsInTheWayOfMove == null)
            {
                Assert.IsNull(actualPositionsInTheWayOfMove);
            }
            else
            {
                Assert.AreEqual(positions.Count, actualPositionsInTheWayOfMove.Count);

                foreach (var postition in positions)
                {
                    bool actualPositionsContainCurrentExpectedPosition = false;

                    foreach (var actualPosition in actualPositionsInTheWayOfMove)
                    {
                        var actualHorizontal = (char)chessBoardPositionType
                    .GetProperty("Horizontal", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(actualPosition);
                        var actualVertical = (int)chessBoardPositionType
                            .GetProperty("Vertical", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(actualPosition);

                        if (actualHorizontal == postition.Item1 && actualVertical == postition.Item2)
                        {
                            actualPositionsContainCurrentExpectedPosition = true;
                            break;
                        }
                    }

                    Assert.That(actualPositionsContainCurrentExpectedPosition);
                }
            }
        }

        [Test]
        [TestCase('c', 8, ChessColors.White, true)]
        [TestCase('d', 8, ChessColors.White, true)]

        [TestCase('c', 8, ChessColors.Black, false)]
        [TestCase('d', 8, ChessColors.Black, false)]

        [TestCase('g', 1, ChessColors.Black, true)]
        [TestCase('a', 1, ChessColors.Black, true)]

        [TestCase('g', 1, ChessColors.White, false)]
        [TestCase('a', 1, ChessColors.White, false)]

        [TestCase('f', 4, ChessColors.White, false)]
        [TestCase('d', 2, ChessColors.White, false)]

        [TestCase('h', 5, ChessColors.Black, false)]
        [TestCase('e', 7, ChessColors.Black, false)]
        public void TestIsPositionProducableMethod(
            char positionHorizontal,
            int positionVertical,
            ChessColors pawnColor,
            bool isProducable)
        {
            var isPositionProducableMethod = this.PawnType.GetMethod("isPositionProducable",
                BindingFlags.Instance | BindingFlags.NonPublic);

            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            bool isPositionProducableActualResult;

            if (pawnColor == ChessColors.Black)
            {
                isPositionProducableActualResult = (bool)isPositionProducableMethod.Invoke(
                        this.BlackPawnInstance,
                        new object[]
                        {
                chessBoardPositionType
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { positionHorizontal, positionVertical}) });
            }
            else
            {
                isPositionProducableActualResult = (bool)isPositionProducableMethod.Invoke(
                        this.WhitePawnInstance,
                        new object[]
                        {
                chessBoardPositionType
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { positionHorizontal, positionVertical}) });
            }

            Assert.AreEqual(isProducable, isPositionProducableActualResult);
        }
    }
}
