using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        private HtmlFormatter _formatter;

        [SetUp]
        public void SetUp()
        {
            _formatter = new HtmlFormatter();
        }

        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var result = _formatter.FormatAsBold("abc");

            //Too specific
            Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            //Too general
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>").IgnoreCase);
            Assert.That(result, Does.Contain("abc").IgnoreCase);
        }
    }
}
