using Enquiries.Models;
using Enquiries.Services;
using Moq;
using Moq.EntityFrameworkCore;

namespace Enquiries.Tests.ServicesTests
{
    public class MediaServiceTests
    {
        [Fact]
        public async Task GetAllMediaAsync_ReturnsAllMedia()
        {
            // Arrange
            var data = new List<Media>
            {
                new() { MediaId = 1, Name = "Media 1" },
                new() { MediaId = 2, Name = "Media 2" },
                new() { MediaId = 3, Name = "Media 3" }
            };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Media).ReturnsDbSet(data);

            var service = new MediaService(mockContext.Object);

            // Act
            var result = await service.GetAllMediaAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data, result);
        }
    }
}
