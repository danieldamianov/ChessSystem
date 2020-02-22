using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace ChessGameLogic.Tests.PositionsTests
{

    [TestFixture]
    public class ChessBoardPositionTests
    {
        public Type ChessBoardPositionType = ChessGameLogicProvider.GetType("ChessGameLogic.ChessBoardPosition");

        [Test]
        [TestCase('a', 0, false)]
        [TestCase('a', 9, false)]
        [TestCase('a', 15, false)]
        [TestCase('3', 5, false)]
        [TestCase('v', 5, false)]
        [TestCase('*', 5, false)]
        [TestCase('a', 1, true)]
        [TestCase('b', 2, true)]
        [TestCase('h', 1, true)]
        [TestCase('d', 8, true)]
        public void TestChessBoardPositionInitialization(char horizontal,int vertical,bool isValid)
        {
            if (isValid == false)
            {
                Assert.That(() => this.ChessBoardPositionType.GetConstructors(System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.NonPublic)[0].Invoke(new object[] { horizontal, vertical }),
                        Throws.InnerException.InstanceOf(ChessGameLogicProvider.GetType("ChessGameLogic.Exceptions.PositionOutOfBoardException"))); 
            }
            else
            {
                var chessBoardPositionInstance = this.ChessBoardPositionType.GetConstructors(BindingFlags.Instance
                    | BindingFlags.NonPublic)[0].Invoke(new object[] { horizontal, vertical });

                var actualHorizontal = (char)this.ChessBoardPositionType
                    .GetProperty("Horizontal", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(chessBoardPositionInstance);
                var actualVertical = (int)this.ChessBoardPositionType
                    .GetProperty("Vertical", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(chessBoardPositionInstance);

                Assert.That(actualHorizontal == horizontal);
                Assert.That(actualVertical == vertical);
            }
        }

        [Test]
        [TestCase('a',1,true)]
        [TestCase('a',2,false)]
        [TestCase('b',1,false)]
        [TestCase('d',8,false)]
        public void TestChessBoardPostitionEqualityMethod(char horizontal, int vertical, bool areEqualExpected)
        {
            var chessBoardPositionInstance = this.ChessBoardPositionType.GetConstructors(BindingFlags.Instance
                    | BindingFlags.NonPublic)[0].Invoke(new object[] { 'a', 1 });

            var chessBoardPositionInstanceToCompareWith = this.ChessBoardPositionType.GetConstructors(BindingFlags.Instance
                    | BindingFlags.NonPublic)[0].Invoke(new object[] { horizontal, vertical });

            var chessBoardPositionEqualsMethod = this.ChessBoardPositionType.GetMethods()
                .Where(m => FilterMethod(m))
                .First();

            var actualEqualityResult = chessBoardPositionEqualsMethod.Invoke(chessBoardPositionInstance, new object[] { chessBoardPositionInstanceToCompareWith });

            Assert.AreEqual(areEqualExpected, actualEqualityResult);
        }

        private bool FilterMethod(MethodInfo m)
        {
            ParameterInfo[] parameters = m.GetParameters();
            return m.Name == "Equals" && parameters.Length == 1
                            && parameters[0].ParameterType == this.ChessBoardPositionType;
        }
    }
}
