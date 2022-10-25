using Gartner;
using Gartner.DataContracts;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Gartner.Tests
{
    public class Tests
    {
        private Mock<ILoggerFactory> _mockFileService = new Mock<ILoggerFactory>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_yaml()
        {
            //Arrange
            bool isComplete =true;
            string fileName = "capterra.yaml";
            var FileService = new FileService(_mockFileService.Object);

            //Act
            FileService.ParseFile(fileName, ref isComplete);

            //Assert
            var result = (bool)isComplete;
        }

        [Test]
        public void Test_json()
        {
            //Arrange
            bool isComplete = true;
            string fileName = "softwareadvice.json";
            var FileService = new FileService(_mockFileService.Object);

            //Act
            FileService.ParseFile(fileName, ref isComplete);

            //Assert
            var result = (bool)isComplete;
            Assert.AreEqual(true, result);
        }
    }
}