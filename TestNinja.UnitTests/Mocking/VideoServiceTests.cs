using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>(); // contine proprietatea Object care e un obiect care implementeaza interfata IFileReader
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }


        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            // Constructor Injection
            //var service = new VideoService(new FakeFileReader());

            // Property Injection
            //service.FileReader = new FakeFileReader();

            // Method parameters Injection
            //var result = service.ReadVideoTitle(new FakeFileReader());

            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_OnlyProcessedVideos_ReturnEmptyString()
        {
            _repository.Setup(repository => repository.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_UnprocessedVideosExist_ReturnUnprocessedVideoIdsAsString()
        {
            _repository.Setup(repository => repository.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
