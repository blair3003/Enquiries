using Enquiries.Models;
using Enquiries.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Net.Sockets;

namespace Enquiries.Tests.ServicesTests
{
    public class EnquiryServiceTests
    {
        [Fact]
        public async Task GetAllEnquiriesAsync_ReturnsAllEnquiries()
        {
            // Arrange
            var data = new List<Enquiry>
            {
                new() { EnquiryId = 1, Subject = "Enquiry 1" },
                new() { EnquiryId = 2, Subject = "Enquiry 2" },
                new() { EnquiryId = 3, Subject = "Enquiry 3" }
            };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Enquiries).ReturnsDbSet(data);

            var service = new EnquiryService(mockContext.Object);

            // Act
            var result = await service.GetAllEnquiriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(data, result);
        }

        [Fact]
        public async Task GetEnquiryByIdAsync_ReturnsSingleEnquiry()
        {
            // Arrange
            var data = new List<Enquiry>
            {
                new() { EnquiryId = 1, Subject = "Enquiry 1" },
                new() { EnquiryId = 2, Subject = "Enquiry 2" },
                new() { EnquiryId = 3, Subject = "Enquiry 3" }
            };

            var mockSet = new Mock<DbSet<Enquiry>>();
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                   .ReturnsAsync((object[] id) => data.FirstOrDefault(d => d.EnquiryId == (int)id[0]));

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Enquiries).Returns(mockSet.Object);

            var service = new EnquiryService(mockContext.Object);

            // Act
            var result = await service.GetEnquiryByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.EnquiryId);
            Assert.Equal("Enquiry 1", result.Subject);
        }

        [Fact]
        public async Task GetEnquiryByIdAsync_ReturnsNull()
        {
            // Arrange
            var data = new List<Enquiry>
            {
                new() { EnquiryId = 1, Subject = "Enquiry 1" },
                new() { EnquiryId = 2, Subject = "Enquiry 2" },
                new() { EnquiryId = 3, Subject = "Enquiry 3" }
            };

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Enquiries).ReturnsDbSet(data);

            var service = new EnquiryService(mockContext.Object);

            // Act
            var result = await service.GetEnquiryByIdAsync(4);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddEnquiryAsync_CreatesNewEnquiry()
        {
            // Arrange
            var newEnquiry = new Enquiry { Subject = "Enquiry 1" };

            var mockSet = new Mock<DbSet<Enquiry>>();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Enquiries).Returns(mockSet.Object);

            var service = new EnquiryService(mockContext.Object);

            // Act
            await service.AddEnquiryAsync(newEnquiry);

            // Assert
            mockSet.Verify(x => x.AddAsync(It.Is<Enquiry>(y => y == newEnquiry), CancellationToken.None), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateEnquiryAsync_ModifiesExistingEnquiry()
        {
            // Arrange            
            var enquiryId = 1;
            var existingEnquiry = new Enquiry { EnquiryId = enquiryId, Subject = "Original Subject", UpdatedOn = DateTime.UtcNow };
            var updatedEnquiry = new Enquiry { EnquiryId = enquiryId, Subject = "Updated Subject" };

            var mockSet = new Mock<DbSet<Enquiry>>();
            mockSet.Setup(m => m.FindAsync(enquiryId)).ReturnsAsync(existingEnquiry);

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(x => x.Enquiries).Returns(mockSet.Object);

            var service = new EnquiryService(mockContext.Object);

            // Act
            await service.UpdateEnquiryAsync(enquiryId, updatedEnquiry);

            // Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal("Updated Subject", existingEnquiry.Subject);
        }
    }
}
