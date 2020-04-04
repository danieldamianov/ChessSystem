namespace ChessGameLogic.Tests.ValidationTests
{
    using System.Reflection;

    using NUnit.Framework;

    [TestFixture]
    public class PositionValidatorTests
    {
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
        public void TestValidatePosition(char horizontal, int verticlal, bool isValid)
        {
            var positionValidatorType = ChessGameLogicProvider.GetType("ChessGameLogic.Validations.PositionValidator");

            var methodValidatePostition = positionValidatorType.GetMethod("ValidatePosition", BindingFlags.Static | BindingFlags.NonPublic);

            var actualValidationResult = (bool)methodValidatePostition
                .Invoke(null, new object[] { horizontal, verticlal });

            Assert.That(isValid == actualValidationResult);
        }
    }
}
