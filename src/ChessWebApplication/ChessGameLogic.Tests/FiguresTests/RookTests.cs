using ChessGameLogic.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChessGameLogic.Tests.FiguresTests
{
    [TestFixture]
    public class RookTests
    {
        public RookTests()
        {
            this.RookType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.Rook");
            this.RookInstance = this.RookType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { ChessColors.White });
        }

        public Type RookType;

        public object RookInstance;

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColor = this.RookType.GetProperty("Color")
                .GetValue(this.RookInstance);

            Assert.That((ChessColors)actualColor == ChessColors.White);
        }

        [Test]
        [TestCase('a', 1, 'a', 5, true)]
        [TestCase('d', 3, 'd', 8, true)]
        [TestCase('a', 7, 'a', 8, true)]
        [TestCase('h', 5, 'h', 3, true)]
        [TestCase('d', 2, 'd', 1, true)]
        [TestCase('c', 8, 'c', 6, true)]
        [TestCase('f', 7, 'h', 7, true)]
        [TestCase('a', 4, 'g', 4, true)]
        [TestCase('c', 1, 'g', 1, true)]
        [TestCase('h', 5, 'b', 5, true)]
        [TestCase('g', 7, 'c', 7, true)]
        [TestCase('d', 3, 'a', 3, true)]
        [TestCase('a', 1, 'h', 8, false)]
        [TestCase('f', 5, 'f', 5, false)]
        [TestCase('d', 1, 'b', 2, false)]
        [TestCase('h', 3, 'a', 1, false)]
        [TestCase('c', 7, 'g', 5, false)]
        [TestCase('b', 3, 'g', 7, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, bool isValid)
        {
            var arePositionsPossibleMethod = this.RookType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            var actualResult = arePositionsPossibleMethod.Invoke(this.RookInstance, new object[] { move });

            Assert.AreEqual(isValid, actualResult);
        }

        [Test]
        [TestCase('a', 1, 'a', 5, new object[] { 'a', 2, 'a', 3, 'a', 4 })]
        [TestCase('d', 3, 'd', 8, new object[] { 'd', 4, 'd', 5, 'd', 6, 'd', 7, })]
        [TestCase('a', 7, 'a', 8, new object[] { })]
        [TestCase('h', 5, 'h', 3, new object[] { 'h', 4 })]
        [TestCase('d', 2, 'd', 1, new object[] { })]
        [TestCase('c', 8, 'c', 6, new object[] { 'c', 7 })]
        [TestCase('f', 7, 'h', 7, new object[] { 'g', 7, })]
        [TestCase('a', 4, 'g', 4, new object[] { 'b', 4, 'c', 4, 'd', 4, 'e', 4, 'f', 4, })]
        [TestCase('c', 1, 'g', 1, new object[] { 'd', 1, 'e', 1, 'f', 1, })]
        [TestCase('h', 5, 'b', 5, new object[] { 'c', 5, 'd', 5, 'e', 5, 'f', 5, 'g', 5, })]
        [TestCase('g', 7, 'c', 7, new object[] { 'd', 7, 'e', 7, 'f', 7, })]
        [TestCase('d', 3, 'a', 3, new object[] { 'b', 3, 'b', 3, })]
        [TestCase('a', 1, 'h', 8, null)]
        [TestCase('f', 5, 'f', 5, null)]
        [TestCase('d', 1, 'b', 2, null)]
        [TestCase('h', 3, 'a', 1, null)]
        [TestCase('c', 7, 'g', 5, null)]
        [TestCase('b', 3, 'g', 7, null)]
        public void TestGetPositionsInTheWayOfMoveMethod(
            char initialHorizontal,
            int initialVertical,
            char targetHorizontal,
            int targetVertical,
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

            var getPositionsInTheWayOfMoveMethod = this.RookType.GetMethod("GetPositionsInTheWayOfMove");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });

            var actualPositionsInTheWayOfMove = (ICollection)getPositionsInTheWayOfMoveMethod.Invoke(this.RookInstance, new object[] { move });

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
        public void TestMoveMehtod()
        {
            var hasBeenMovedFromTheStartOfTheGameProperty = this.RookType.GetProperty("HasBeenMovedFromTheStartOfTheGame", BindingFlags.Instance | BindingFlags.Public);
            var initialValue = (bool)hasBeenMovedFromTheStartOfTheGameProperty.GetValue(this.RookInstance);

            Assert.That(initialValue == false);

            var moveMethod = this.RookType.GetMethod("Move");

            moveMethod.Invoke(this.RookInstance,new object[] { });

            var finalValue = (bool)hasBeenMovedFromTheStartOfTheGameProperty.GetValue(this.RookInstance);

            Assert.That(finalValue == true);
        }
    }
}
