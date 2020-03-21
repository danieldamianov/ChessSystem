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
    public class BishopTests
    {
        public BishopTests()
        {
            this.BishopType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.Bishop");
            this.BishopInstance = this.BishopType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { ChessColors.White });
        }

        public Type BishopType;

        public object BishopInstance;

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColor = this.BishopType.GetProperty("Color")
                .GetValue(this.BishopInstance);

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
        [TestCase('a', 1, 'g', 1, false)]
        [TestCase('f', 5, 'f', 5, false)]
        [TestCase('d', 1, 'd', 8, false)]
        [TestCase('h', 3, 'a', 1, false)]
        [TestCase('c', 7, 'g', 5, false)]
        [TestCase('b', 3, 'g', 7, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, bool isValid)
        {
            var arePositionsPossibleMethod = this.BishopType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            var actualResult = arePositionsPossibleMethod.Invoke(this.BishopInstance, new object[] { move });

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
        [TestCase('a', 1, 'g', 1, null)]
        [TestCase('f', 5, 'f', 5, null)]
        [TestCase('d', 1, 'd', 8, null)]
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

            var getPositionsInTheWayOfMoveMethod = this.BishopType.GetMethod("GetPositionsInTheWayOfMove");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });

            var actualPositionsInTheWayOfMove = (ICollection)getPositionsInTheWayOfMoveMethod.Invoke(this.BishopInstance, new object[] { move });

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
