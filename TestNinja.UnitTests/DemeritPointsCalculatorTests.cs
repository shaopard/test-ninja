using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;

        [SetUp]
        public void SetUp() => _demeritPointsCalculator = new DemeritPointsCalculator();

        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_SpeedIsOutsideRange_ThrowsArgumentOutOfRangeException(int outOfRangeSpeed)
        {
            Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(outOfRangeSpeed), 
                Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(64, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(67, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int outOfRangeSpeed, int expectedResult)
        { 
            var result = _demeritPointsCalculator.CalculateDemeritPoints(outOfRangeSpeed);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
