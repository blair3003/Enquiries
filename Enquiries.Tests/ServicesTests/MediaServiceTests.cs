using Enquiries.Models;
using Enquiries.Services;
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task GetMediaByIdAsync_ReturnsSingleMedia()
        {
            // Arrange
            var data = new List<Media>
            {
                new() { MediaId = 1, Name = "Media 1" },
                new() { MediaId = 2, Name = "Media 2" },
                new() { MediaId = 3, Name = "Media 3" }
            };

            var mockSet = new Mock<DbSet<Media>>();
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                   .ReturnsAsync((object[] id) => data.FirstOrDefault(d => d.MediaId == (int)id[0]));

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Media).Returns(mockSet.Object);

            var service = new MediaService(mockContext.Object);

            // Act
            var result = await service.GetMediaByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.MediaId);
            Assert.Equal("Media 1", result.Name);
        }

        [Fact]
        public async Task GetMediaByIdAsync_ReturnsNull()
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
            var result = await service.GetMediaByIdAsync(4);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddMediaAsync_CreatesNewMedia()
        {
            // Arrange
            var newMedia = new Media { Name = "Media 1" };

            var mockSet = new Mock<DbSet<Media>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Media).Returns(mockSet.Object);

            var service = new MediaService(mockContext.Object);

            // Act
            await service.AddMediaAsync(newMedia);

            // Assert
            mockSet.Verify(x => x.AddAsync(It.Is<Media>(y => y == newMedia), CancellationToken.None), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateMediaAsync_ModifiesExistingMedia()
        {
            // Arrange            
            var mediaId = 1;
            var existingMedia = new Media { MediaId = mediaId, Name = "Original Name" };
            var updatedMedia = new Media { MediaId = mediaId, Name = "Updated Name" };

            var mockSet = new Mock<DbSet<Media>>();
            mockSet.Setup(m => m.FindAsync(mediaId)).ReturnsAsync(existingMedia);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Media).Returns(mockSet.Object);

            var service = new MediaService(mockContext.Object);

            // Act
            await service.UpdateMediaAsync(mediaId, updatedMedia);

            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("Updated Name", existingMedia.Name);
        }
    }
}
