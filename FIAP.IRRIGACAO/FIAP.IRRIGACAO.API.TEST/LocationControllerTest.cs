using FIAP.IRRIGACAO.API.Controllers;
using FIAP.IRRIGACAO.API.Service;
using FIAP.IRRIGACAO.API.ViewModel;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FIAP.IRRIGACAO.API.Tests.Controllers
{
    public class LocationControllerTests
    {
        private readonly Mock<ILocationService> _mockLocationService;
        private readonly LocationController _controller;

        public LocationControllerTests()
        {
            _mockLocationService = new Mock<ILocationService>();
            _controller = new LocationController(_mockLocationService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk_WhenItemsExist()
        {
            // Arrange
            var locationViewModels = new List<LocationViewModel>
            {
                new LocationViewModel { Id = 1, Name = "Location 1" },
                new LocationViewModel { Id = 2, Name = "Location 2" }
            };

            _mockLocationService.Setup(service => service.GetAll())
                                .Returns(locationViewModels);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<LocationViewModel>>(okResult.Value);
            Assert.Equal(locationViewModels.Count, returnValue.Count);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenItemExists()
        {
            // Arrange
            var locationViewModel = new LocationViewModel { Id = 1, Name = "Location 1" };
            _mockLocationService.Setup(service => service.FindById(1))
                                .Returns(locationViewModel);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<LocationViewModel>(okResult.Value);
            Assert.Equal(locationViewModel.Id, returnValue.Id);
        }

        [Fact]
        public void Create_ShouldReturnCreated_WhenModelIsValid()
        {
            // Arrange
            var locationViewModel = new LocationRegisterViewModel
            {
                Name = "New Location"
            };

            var createdLocation = new LocationViewModel
            {
                Id = 1,
                Name = "New Location"
            };

            _mockLocationService.Setup(service => service.Create(It.IsAny<LocationRegisterViewModel>()))
                                .Returns(createdLocation);

            // Act
            var result = _controller.Create(locationViewModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<LocationViewModel>(createdResult.Value);
            Assert.Equal(createdLocation.Id, returnValue.Id);
        }

        [Fact]
        public void Delete_ShouldReturnOk_WhenItemIsDeletedSuccessfully()
        {
            // Arrange
            var locationId = 1L;
            var location = new LocationViewModel { Id = locationId, Name = "Location 1" };
            _mockLocationService.Setup(s => s.DeleteAndReturn(locationId)).Returns(location);

            // Act
            var result = _controller.Delete(locationId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(location);
        }

        [Fact]
        public void Update_ShouldReturnNoContent_WhenItemIsUpdatedSuccessfully()
        {
            // Arrange
            var locationId = 1L;
            var model = new LocationRegisterViewModel { Name = "Updated Location" };
            _mockLocationService.Setup(s => s.Update(locationId, model)).Verifiable();

            // Act
            var result = _controller.Update(locationId, model);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
            _mockLocationService.Verify(s => s.Update(locationId, model), Times.Once);
        }
    }
}
