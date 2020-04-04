using ChessGameLogic.Enums;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChessGameLogic.Tests.FiguresTests
{
    [TestFixture]
    public class QueenTests
    {
        public QueenTests()
        {
            this.QueenType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.Queen");
            this.QueenInstance = this.QueenType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { ChessColors.White });
        }

        public Type QueenType { get; set; }

        public object QueenInstance { get; set; }

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColor = this.QueenType.GetProperty("Color")
                .GetValue(this.QueenInstance);

            Assert.That((ChessColors)actualColor == ChessColors.White);
        }

        [Test]
        [TestCase('a', 1, 'h', 8, true)]
        [TestCase('d', 3, 'f', 5, true)]
        [TestCase('a', 7, 'b', 8, true)]
        [TestCase('h', 5, 'f', 7, true)]
        [TestCase('d', 2, 'b', 4, true)]
        [TestCase('g', 1, 'b', 6, true)]
        [TestCase('f', 7, 'a', 2, true)]
        [TestCase('d', 8, 'a', 5, true)]
        [TestCase('h', 2, 'g', 1, true)]
        [TestCase('b', 5, 'd', 3, true)]
        [TestCase('e', 7, 'h', 4, true)]
        [TestCase('e', 4, 'h', 1, true)]
        [TestCase('a', 1, 'b', 3, false)]
        [TestCase('f', 5, 'f', 5, false)]
        [TestCase('d', 1, 'c', 8, false)]
        [TestCase('h', 3, 'a', 1, false)]
        [TestCase('c', 7, 'g', 5, false)]
        [TestCase('b', 3, 'g', 7, false)]

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
        [TestCase('a', 1, 'h', 7, false)]
        [TestCase('d', 1, 'b', 2, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, bool isValid)
        {
            var arePositionsPossibleMethod = this.QueenType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            var actualResult = arePositionsPossibleMethod.Invoke(this.QueenInstance, new object[] { move });

            Assert.AreEqual(isValid, actualResult);
        }

        [Test]
        [TestCase('a', 1, 'h', 8, new object[] { 'b', 2, 'c', 3, 'd', 4, 'e', 5, 'f', 6, 'g', 7 })]
        [TestCase('d', 3, 'f', 5, new object[] { 'e', 4 })]
        [TestCase('a', 7, 'b', 8, new object[] { })]
        [TestCase('h', 5, 'f', 7, new object[] { 'g', 6 })]
        [TestCase('d', 2, 'b', 4, new object[] { 'c', 3 })]
        [TestCase('g', 1, 'b', 6, new object[] { 'f', 2, 'e', 3, 'd', 4, 'c', 5 })]
        [TestCase('f', 7, 'a', 2, new object[] { 'e', 6, 'd', 5, 'c', 4, 'b', 3 })]
        [TestCase('d', 8, 'a', 5, new object[] { 'c', 7, 'b', 6 })]
        [TestCase('h', 2, 'g', 1, new object[] { })]
        [TestCase('b', 5, 'd', 3, new object[] { 'c', 4 })]
        [TestCase('e', 7, 'h', 4, new object[] { 'f', 6, 'g', 5 })]
        [TestCase('e', 4, 'h', 1, new object[] { 'f', 3, 'g', 2 })]
        [TestCase('a', 1, 'b', 3, null)]
        [TestCase('f', 5, 'f', 5, null)]
        [TestCase('d', 1, 'c', 8, null)]
        [TestCase('h', 3, 'a', 1, null)]
        [TestCase('c', 7, 'g', 5, null)]
        [TestCase('b', 3, 'g', 7, null)]

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
        [TestCase('a', 1, 'h', 7, null)]
        [TestCase('d', 1, 'b', 2, null)]
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

            var getPositionsInTheWayOfMoveMethod = this.QueenType.GetMethod("GetPositionsInTheWayOfMove");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0]
                .Invoke(new object[] { initialHorizontal, initialVertical, targetHorizontal, targetVertical, });

            var actualPositionsInTheWayOfMove = (ICollection)getPositionsInTheWayOfMoveMethod.Invoke(this.QueenInstance, new object[] { move });

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
    }
}
