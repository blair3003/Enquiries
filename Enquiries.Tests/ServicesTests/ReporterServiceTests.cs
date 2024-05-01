using Enquiries.Models;
using Enquiries.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Enquiries.Tests.ServicesTests
{
    public class ReporterServiceTests
    {
        [Fact]
        public async Task GetAllReportersAsync_ReturnsAllReporters()
        {
            // Arrange
            var data = new List<Reporter>
            {
                new() { ReporterId = 1, Name = "Reporter 1" },
                new() { ReporterId = 2, Name = "Reporter 2" },
                new() { ReporterId = 3, Name = "Reporter 3" }
            };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Reporters).ReturnsDbSet(data);

            var service = new ReporterService(mockContext.Object);

            // Act
            var result = await service.GetAllReportersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data, result);
        }

        [Fact]
        public async Task GetReporterByIdAsync_ReturnsSingleReporter()
        {
            // Arrange
            var data = new List<Reporter>
            {
                new() { ReporterId = 1, Name = "Reporter 1" },
                new() { ReporterId = 2, Name = "Reporter 2" },
                new() { ReporterId = 3, Name = "Reporter 3" }
            };

            var mockSet = new Mock<DbSet<Reporter>>();
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                   .ReturnsAsync((object[] id) => data.FirstOrDefault(d => d.ReporterId == (int)id[0]));

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Reporters).Returns(mockSet.Object);

            var service = new ReporterService(mockContext.Object);

            // Act
            var result = await service.GetReporterByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.ReporterId);
            Assert.Equal("Reporter 1", result.Name);
        }

        [Fact]
        public async Task GetReporterByIdAsync_ReturnsNull()
        {
            // Arrange
            var data = new List<Reporter>
            {
                new() { ReporterId = 1, Name = "Reporter 1" },
                new() { ReporterId = 2, Name = "Reporter 2" },
                new() { ReporterId = 3, Name = "Reporter 3" }
            };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Reporters).ReturnsDbSet(data);

            var service = new ReporterService(mockContext.Object);

            // Act
            var result = await service.GetReporterByIdAsync(4);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddReporterAsync_CreatesNewReporter()
        {
            // Arrange
            var newReporter = new Reporter { Name = "Reporter 1" };

            var mockSet = new Mock<DbSet<Reporter>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Reporters).Returns(mockSet.Object);

            var service = new ReporterService(mockContext.Object);

            // Act
            await service.AddReporterAsync(newReporter);

            // Assert
            mockSet.Verify(x => x.AddAsync(It.Is<Reporter>(y => y == newReporter), CancellationToken.None), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateReporterAsync_ModifiesExistingReporter()
        {
            // Arrange            
            var reporterId = 1;
            var existingReporter = new Reporter { ReporterId = reporterId, Name = "Original Name" };
            var updatedReporter = new Reporter { ReporterId = reporterId, Name = "Updated Name" };

            var mockSet = new Mock<DbSet<Reporter>>();
            mockSet.Setup(m => m.FindAsync(reporterId)).ReturnsAsync(existingReporter);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Reporters).Returns(mockSet.Object);

            var service = new ReporterService(mockContext.Object);

            // Act
            await service.UpdateReporterAsync(reporterId, updatedReporter);

            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("Updated Name", existingReporter.Name);
        }
    }
}
