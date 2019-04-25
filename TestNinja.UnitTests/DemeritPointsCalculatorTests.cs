using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalculator;

        [TestInitialize]
        public void SetUp() => _demeritPointsCalculator = new DemeritPointsCalculator();

        [TestMethod]
        [DataRow(-1)]
        [DataRow(301)]
        public void CalculateDemeritPoints_SpeedIsOutsideRange_ThrowsArgumentOutOfRangeException(int outOfRangeSpeed)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _demeritPointsCalculator.CalculateDemeritPoints(outOfRangeSpeed));

            //Assert.That(() => _demeritPointsCalculator.CalculateDemeritPoints(outOfRangeSpeed), 
            //    Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(64, 0)]
        [DataRow(65, 0)]
        [DataRow(66, 0)]
        [DataRow(67, 0)]
        [DataRow(70, 1)]
        [DataRow(75, 2)]
        public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int outOfRangeSpeed, int expectedResult)
        { 
            var result = _demeritPointsCalculator.CalculateDemeritPoints(outOfRangeSpeed);

            Assert.AreEqual(result, expectedResult);

            //Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
