namespace ChessGameLogic.Tests.ChessMovesTests
{
    using System.Linq;
    using System.Reflection;

    using NUnit.Framework;

    [TestFixture]
    public class NormalChessMoveTests
    {
        [Test]
        [TestCase('a', 1, 'b', 2, true)]
        [TestCase('*', 1, 'b', 2, false)]
        [TestCase('t', 0, 'b', 2, false)]
        [TestCase('a', 1, 'l', 2, false)]
        [TestCase('a', 1, 'b', 123, false)]
        public void TestNormalChessMoveSettingPositionsCorrect(
            char initialPositionHorizontal,
            int initialPositionVertical,
            char targetPositionHorizontal,
            int targetPositionVertical,
            bool isValid)
        {
            var normalChessMoveType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessMoves.NormalChessMovePositions");
            var chessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

            if (isValid == false)
            {
                Assert.That(
                    () => normalChessMoveType
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0]
                .Invoke(new object[]
                {
                    initialPositionHorizontal,
                    initialPositionVertical,
                    targetPositionHorizontal,
                    targetPositionVertical,
                }),
                    Throws.InnerException.InstanceOf(ChessGameLogicProvider.GetType("ChessGameLogic.Exceptions.PositionOutOfBoardException")));
            }
            else
            {
                var normalChessMoveInstance = normalChessMoveType.GetConstructors(BindingFlags.Instance
                    | BindingFlags.NonPublic)[0].Invoke(new object[]
                    {
                        initialPositionHorizontal,
                        initialPositionVertical,
                        targetPositionHorizontal,
                        targetPositionVertical,
                    });

                var initialPosition = normalChessMoveType
                    .GetProperty("InitialPosition", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(normalChessMoveInstance);
                var targetPosition = normalChessMoveType
                    .GetProperty("TargetPosition", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(normalChessMoveInstance);

                var chessBoardPositionEqualsMethod = chessBoardPositionType.GetMethods()
                .Where(m => this.FilterMethod(m))
                .First();

                object initialChessBoardPositionInstance = chessBoardPositionType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0]
                    .Invoke(new object[]
                    {
                        initialPositionHorizontal,
                        initialPositionVertical,
                    });

                Assert.That((bool)chessBoardPositionEqualsMethod
                    .Invoke(
                    initialPosition,
                    new object[] { initialChessBoardPositionInstance, }));

                object targetChessBoardPositionInstance = chessBoardPositionType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0]
                    .Invoke(new object[]
                    {
                        targetPositionHorizontal,
                        targetPositionVertical,
                    });

                Assert.That((bool)chessBoardPositionEqualsMethod
                    .Invoke(targetPosition, new object[] { targetChessBoardPositionInstance }));
            }
        }

        private bool FilterMethod(MethodInfo m)
        {
            ParameterInfo[] parameters = m.GetParameters();
            return m.Name == "Equals" && parameters.Length == 1
                            && parameters[0].ParameterType == m.DeclaringType;
        }
    }
}
