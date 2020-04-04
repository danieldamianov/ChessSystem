namespace ChessGameLogic.Tests.FiguresTests
{
    using ChessGameLogic.Enums;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class KingTests
    {
        public KingTests()
        {
            this.KingType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessFigures.King");
            this.KingInstance = this.KingType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .First()
                .Invoke(new object[] { ChessColors.White });
        }

        public Type KingType { get; set; }

        public object KingInstance { get; set; }

        [Test]
        public void TestIfColorIsSetCorrectly()
        {
            var actualColor = this.KingType.GetProperty("Color")
                .GetValue(this.KingInstance);

            Assert.That((ChessColors)actualColor == ChessColors.White);
        }

        [Test]
        [TestCase('f', 5, 'e', 5, true)]
        [TestCase('f', 5, 'g', 5, true)]
        [TestCase('f', 5, 'f', 6, true)]
        [TestCase('f', 5, 'f', 4, true)]
        [TestCase('f', 5, 'e', 6, true)]
        [TestCase('f', 5, 'g', 4, true)]
        [TestCase('f', 5, 'g', 6, true)]
        [TestCase('f', 5, 'e', 4, true)]
        [TestCase('f', 5, 'f', 5, false)]
        [TestCase('f', 5, 'b', 2, false)]
        [TestCase('f', 5, 'a', 1, false)]
        [TestCase('f', 5, 'g', 8, false)]
        [TestCase('f', 5, 'g', 7, false)]
        public void TestAreMovePositionsPossibleMethod(char initialHorizontal,
            int initialVertical, char targetHorizontal, int targetVertical, bool isValid)
        {
            var arePositionsPossibleMethod = this.KingType.GetMethod("AreMovePositionsPossible");

            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");

            var move = normalChessMoveType.GetConstructors(BindingFlags.Instance
                | BindingFlags.NonPublic)[0].Invoke(new object[] { initialHorizontal,initialVertical
                , targetHorizontal, targetVertical });


            var actualResult = arePositionsPossibleMethod.Invoke(this.KingInstance, new object[] { move });

            Assert.AreEqual(isValid, actualResult);
        }

        [Test]
        public void TestMoveMehtod()
        {
            var hasBeenMovedFromTheStartOfTheGameProperty = this.KingType.GetProperty("HasBeenMovedFromTheStartOfTheGame", BindingFlags.Instance | BindingFlags.Public);
            var initialValue = (bool)hasBeenMovedFromTheStartOfTheGameProperty.GetValue(this.KingInstance);

            Assert.That(initialValue == false);

            var moveMethod = this.KingType.GetMethod("Move");

            moveMethod.Invoke(this.KingInstance, new object[] { });

            var finalValue = (bool)hasBeenMovedFromTheStartOfTheGameProperty.GetValue(this.KingInstance);

            Assert.That(finalValue == true);
        }
    }
}
