using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class ProductTests
    {
        private Mock<ICustomer> _customer;

        [SetUp]
        public void SetUp()
        {
            _customer = new Mock<ICustomer>();
        }

        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            var product = new Product { ListPrice = 100 };

            var result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
        {
            var product = new Product { ListPrice = 100 };
            _customer.Setup(c => c.IsGold).Returns(true);

            var result = product.GetPrice(_customer.Object);

            Assert.That(result, Is.EqualTo(70));
        }
    }
}
