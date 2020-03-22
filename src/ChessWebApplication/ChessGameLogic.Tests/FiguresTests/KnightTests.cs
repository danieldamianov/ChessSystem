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
    public class KnightTests
    {
        public KnightTests()
        {
            this.KnightType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.Knight");
            this.KnightInstance = this.KnightType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { ChessColors.White });
        }

        public Type KnightType;

        public object KnightInstance;

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColor = this.KnightType.GetProperty("Color")
                .GetValue(this.KnightInstance);

            Assert.That((ChessColors)actualColor == ChessColors.White);
        }

        [Test]
        [TestCase('e', 5, 'd', 7, true)]
        [TestCase('e', 5, 'f', 7, true)]
        [TestCase('e', 5, 'g', 4, true)]
        [TestCase('e', 5, 'g', 6, true)]
        [TestCase('e', 5, 'd', 3, true)]
        [TestCase('e', 5, 'f', 3, true)]
        [TestCase('e', 5, 'c', 4, true)]
        [TestCase('e', 5, 'c', 6, true)]
        [TestCase('a', 1, 'g', 1, false)]
        [TestCase('f', 5, 'f', 5, false)]
        [TestCase('d', 1, 'd', 8, false)]
        [TestCase('h', 3, 'a', 1, false)]
        [TestCase('c', 7, 'g', 5, false)]
        [TestCase('b', 3, 'g', 7, false)]
        [TestCase('e', 5, 'e', 7, false)]
        [TestCase('e', 5, 'g', 5, false)]
        [TestCase('e', 5, 'd', 2, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, bool isValid)
        {
            var arePositionsPossibleMethod = this.KnightType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0]
                .Invoke(new object[] { initialHorizontal, initialVertical, targetHorizontal, targetVertical });


            var actualResult = arePositionsPossibleMethod.Invoke(this.KnightInstance, new object[] { move });

            Assert.AreEqual(isValid, actualResult);
        }
    }
}
