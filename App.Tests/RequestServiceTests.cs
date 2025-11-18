using Xunit;
using Moq;
using App.Application.Services;
using App.Application.DTOs;
using App.Domain.Models;
using App.Domain.Interfaces; 
using System.Threading.Tasks;
using Azure.Core;

namespace App.Tests
{
    public class RequestServiceTests
    {
        [Fact]
        public async Task CreateRequest_ShouldReturnTrue_WhenRequestIsCreatedSuccessfully()
        {
            // Arrange
            var mockRepo = new Mock<IRequestRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Request>())).ReturnsAsync(true);

            var service = new RequestService(mockRepo.Object);

            var dto = new CreateRequestDto
            {
                BloodBankId = 1,
                Quantity = 2.5m,
                BloodType = "A+",
                PatientName = "Marwa",
                PatientPhoneNumber = "0123456789",
                NationalId = "12345678901234"
            };

            // Act
            var result = await service.CreateRequest(dto);

            // Assert
            Assert.True(result);
        }
    }
}
