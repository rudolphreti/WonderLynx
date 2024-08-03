using Xunit;
using Moq;
using API.Services;
using API.Models;
using API.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WonderLynx.Tests
{
    public class ReferenceItemsControllerTests
    {
        private readonly Mock<IReferenceItemGetService> _mockGetService;
        private readonly Mock<IReferenceItemAddService> _mockAddService;
        private readonly Mock<IReferenceItemUpdateService> _mockUpdateService;
        private readonly Mock<IReferenceItemDeleteService> _mockDeleteService;
        private readonly ReferenceItemsController _controller;

        public ReferenceItemsControllerTests()
        {
            _mockGetService = new Mock<IReferenceItemGetService>();
            _mockAddService = new Mock<IReferenceItemAddService>();
            _mockUpdateService = new Mock<IReferenceItemUpdateService>();
            _mockDeleteService = new Mock<IReferenceItemDeleteService>();

            _controller = new ReferenceItemsController(
                _mockGetService.Object,
                _mockAddService.Object,
                _mockUpdateService.Object,
                _mockDeleteService.Object
            );
        }

        [Fact]
        public async Task AddNewReferenceItem_ShouldReturnCreatedResult()
        {
            // Arrange
            var newItem = new ReferenceItem
            {
                Title = "Test Title",
                Subtitle = "Test Subtitle",
                TypeId = null,
                CategoryId = null, // Beispielwert, anpassen nach Bedarf
                Description = "This is a test description for the new reference item.",
                ThumbnailUrl = "https://example.com/thumbnail.png", // Beispiel-URL                                                
            };

            _mockAddService.Setup(service => service.AddAsync(It.IsAny<ReferenceItem>()))
               .ReturnsAsync(newItem);

            // Act
            var result = await _controller.PostReferenceItem(newItem);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ReferenceItem>(actionResult.Value);
            Assert.Equal("Test Title", returnValue.Title);
        }
    }
}
