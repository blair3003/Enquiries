using Enquiries.Models;
using Enquiries.Services;
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
    }
}
