using ChessGameLogic.Enums;
using NUnit.Framework;
using System;
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
            this.BishopInstance = this.BishopType.GetConstructors(System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.NonPublic)
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
    }
}
