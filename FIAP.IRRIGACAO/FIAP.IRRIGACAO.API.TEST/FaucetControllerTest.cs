using Moq;
using FIAP.IRRIGACAO.API.Controllers;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Service;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FluentAssertions;

namespace FIAP.IRRIGACAO.API.Tests.Controllers
{
    public class FaucetControllerTests
    {
        private readonly Mock<IFaucetService> _mockFaucetService;
        private readonly FaucetController _controller;

        public FaucetControllerTests()
        {
            _mockFaucetService = new Mock<IFaucetService>();
            _controller = new FaucetController(_mockFaucetService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnOk_WhenItemsExist()
        {
            // Arrange
            var faucetViewModels = new List<FaucetViewModel>
            {
                new FaucetViewModel { Id = 1, Name = "Faucet 1", IsEnabled = true, LocationId = 1 },
                new FaucetViewModel { Id = 2, Name = "Faucet 2", IsEnabled = false, LocationId = 2 }
            };

            _mockFaucetService.Setup(service => service.GetAll())
                              .Returns(faucetViewModels);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<List<FaucetViewModel>>(okResult.Value);
            Assert.Equal(faucetViewModels.Count, returnValue.Count);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenItemExists()
        {
            // Arrange
            var faucetViewModel = new FaucetViewModel { Id = 1, Name = "Faucet 1", IsEnabled = true, LocationId = 1 };
            _mockFaucetService.Setup(service => service.FindById(1))
                              .Returns(faucetViewModel);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<FaucetViewModel>(okResult.Value);
            Assert.Equal(faucetViewModel.Id, returnValue.Id);
        }

        [Fact]
        public void Create_ShouldReturnCreated_WhenModelIsValid()
        {
            // Arrange
            var faucetViewModel = new FaucetRegisterViewModel
            {
                Name = "New Faucet",
                IsEnabled = true,
                LocationId = 1
            };

            var createdFaucet = new FaucetViewModel
            {
                Id = 1,
                Name = "New Faucet",
                IsEnabled = true,
                LocationId = 1
            };

            _mockFaucetService.Setup(service => service.Create(It.IsAny<FaucetRegisterViewModel>()))
                              .Returns(createdFaucet);

            // Act
            var result = _controller.Create(faucetViewModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<FaucetViewModel>(createdResult.Value);
            Assert.Equal(createdFaucet.Id, returnValue.Id);
        }

        [Fact]
        public void Delete_ShouldReturnOk_WhenItemIsDeletedSuccessfully()
        {
            // Arrange
            var faucetId = 1L;
            var faucet = new FaucetViewModel { Id = faucetId, Name = "Faucet 1", IsEnabled = true };
            _mockFaucetService.Setup(s => s.DeleteAndReturn(faucetId)).Returns(faucet);

            // Act
            var result = _controller.Delete(faucetId);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(faucet);
        }

        [Fact]
        public void Update_ShouldReturnNoContent_WhenItemIsUpdatedSuccessfully()
        {
            // Arrange
            var faucetId = 1L;
            var model = new FaucetRegisterViewModel { Name = "Updated Faucet", IsEnabled = true };
            _mockFaucetService.Setup(s => s.Update(faucetId, model)).Verifiable();

            // Act
            var result = _controller.Update(faucetId, model);

            // Assert
            var noContentResult = result as NoContentResult;
            noContentResult.Should().NotBeNull();
            noContentResult.StatusCode.Should().Be(204);
            _mockFaucetService.Verify(s => s.Update(faucetId, model), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnCreatedAtAction_WhenItemIsCreatedSuccessfully()
        {
            // Arrange
            var model = new FaucetRegisterViewModel { Name = "New Faucet", IsEnabled = true };
            var createdFaucet = new FaucetViewModel { Id = 1, Name = "New Faucet", IsEnabled = true };
            _mockFaucetService.Setup(s => s.Create(model)).Returns(createdFaucet);

            // Act
            var result = _controller.Create(model);

            // Assert
            var createdAtActionResult = result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.ActionName.Should().Be(nameof(_controller.GetById));
            createdAtActionResult.RouteValues["id"].Should().Be(createdFaucet.Id);
            createdAtActionResult.Value.Should().Be(createdFaucet);
            _mockFaucetService.Verify(s => s.Create(model), Times.Once);
        }

    }
}