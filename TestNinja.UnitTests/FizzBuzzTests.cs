using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        private FizzBuzz _fizzBuzz;

        [Test]
        [TestCase(0, "FizzBuzz")]
        [TestCase(1, "1")]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        public void GetOutput_WhenCalled_ReturnExpectedResult(int numberToTest, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(numberToTest);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
