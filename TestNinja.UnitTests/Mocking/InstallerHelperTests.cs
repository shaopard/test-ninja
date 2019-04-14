using Moq;
using NUnit.Framework;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            // la setup in situatii de aruncat exceptii, parametrii trebuie sa fie perfect potriviti cu parametrii cu care se apeleaza metoda
            // daca nu se intampla asta, nu se va executa nimic
            //_fileDownloader.Setup(downloader => downloader.DownloadFile($"http://example.com/customer/installer", null)).Throws<WebException>();

            _fileDownloader.Setup(downloader => 
                downloader.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var result =_installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void DownloadInstaller_DownloadSucceeds_ReturnTrue()
        {
            _fileDownloader.Setup(downloader => downloader.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.EqualTo(false));
        }
    }
}
