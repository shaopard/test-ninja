using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestNinja.ConceptClasses;

namespace TestNinja.UnitTests.ConceptClasses
{
    [TestClass]
    public class ClassicRecursionTests
    {
        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 1)]
        [DataRow(3, 2)]
        [DataRow(4, 3)]
        [DataRow(5, 5)]
        [DataRow(6, 8)]
        [DataRow(7, 13)]
        [DataRow(8, 21)]
        public void GetFibonacciNumber_WhenCalled_ReturnsFibonacciNumber(int testIndex, int expectedResult)
        {
            var result = ClassicRecursion.GetFibonacciNumber(testIndex);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(3, 6)]
        [DataRow(4, 24)]
        [DataRow(5, 120)]
        [DataRow(6, 720)]
        public void CalculateFactorial_PositiveInteger_ReturnsFactorialOfArgument(int testIndex, int expectedResult)
        {
            var result = ClassicRecursion.CalculateFactorial(testIndex);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(-1)]
        [DataRow(-21)]
        public void CalculateFactorial_NegativeNumber_ThrowsArgumentOutOfRangeException(int outOfRangeTestIndex)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => ClassicRecursion.CalculateFactorial(outOfRangeTestIndex));
        }
    }
}
