using Enquiries.Models;
using Enquiries.Services;
using Moq;
using Moq.EntityFrameworkCore;

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
    }
}
