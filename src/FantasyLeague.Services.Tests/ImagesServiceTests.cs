using FantasyLeague.Common.Constants;
using FantasyLeague.Models;
using FantasyLeague.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using Xunit;

namespace FantasyLeague.Services.Tests
{
    public class ImagesServiceTests : BaseServiceTests
    {
        public ImagesServiceTests()
        {
        }

        [Theory]
        [InlineData("Player", "some data", "some data")]
        [InlineData("Team", "some data 2", "some data 3")]
        [InlineData("Player", "some/data", "some/data")]
        [InlineData("Team", "some.data", "some.data")]
        public void Create_WithValidData_ShouldReturnImage(
            string type,
            string publicId,
            string url)
        {
            var image = new Image
            {
                ImageType = type,
                PublicId = publicId,
                Url = url
            };

            var imagesService = new Mock<IImagesService>();
            imagesService.Setup(x => x.CreateAsync(type, publicId, url))
                .ReturnsAsync(image);

            this.TearDown();
        }

        [Theory]
        [InlineData("", "", "some data")]
        [InlineData("Something", "some data 2", "some data 3")]
        [InlineData("Kek", "", "")]
        [InlineData("", "some.data", "some.data")]
        [InlineData("", "some.data", "")]
        [InlineData("", "", "")]
        [InlineData("Team", "", "")]
        [InlineData("Player", "", "")]
        [InlineData("  ", "   ", "  ")]
        [InlineData("       ", "        ", "        ")]
        [InlineData(null, null, null)]
        public void Create_WithValidData_ShouldReturnNull(
            string type,
            string publicId,
            string url)
        {
            Image image = null;

            var imagesService = new Mock<IImagesService>();
            imagesService.Setup(x => x.CreateAsync(type, publicId, url))
                .ReturnsAsync(image);

            this.TearDown();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("   ")]
        [InlineData(null)]
        [InlineData("Something")]
        [InlineData("KEK")]
        public void Upload_WithInvalidType_ShouldReturnNull(string type)
        {
            IImageUploadResult result = null; 
            var imagesService = new Mock<IImagesService>();
            imagesService.Setup(x => x.Upload(new Mock<IFormFile>().Object, type))
                .Returns(result);
        }

        [Theory]
        [InlineData("Player")]
        [InlineData("Team")]
        public void Upload_WithValidType_ShouldReturnImageUploadResult(string type)
        {
            var imagesService = new Mock<IImagesService>();
            imagesService.Setup(x => x.Upload(new Mock<IFormFile>().Object, type))
                .ShouldNotBeNull();
        }

        [Fact]
        public void Upload_WithInvalidFormFile_ShouldReturnNull()
        {
            IImageUploadResult result = null;
            var imagesService = new Mock<IImagesService>();
            imagesService.Setup(x => x.Upload(null, GlobalConstants.PlayerName))
                .Returns(result);
        }
    }
}
