using FIAP.IRRIGACAO.API.Controllers;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FIAP.IRRIGACAO.API.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserStore<ApplicationUser>> _mockUserStore;
        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private UserController _controller;

        public UserControllerTests()
        {
            _mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                _mockUserStore.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                _mockUserManager.Object,
                Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null,
                null,
                null,
                null
            );
            _controller = new UserController(_mockSignInManager.Object, _mockUserManager.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var model = new LoginViewModel
            {
                Email = "user@example.com",
                Password = "password123",
                RememberMe = true
            };

            _mockSignInManager
                .Setup(sm => sm.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var messageProperty = returnValue?.GetType().GetProperty("Message");
            Assert.NotNull(messageProperty);
            Assert.Equal("Login realizado com sucesso.", messageProperty.GetValue(returnValue)?.ToString());
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var model = new UserRegisterViewModel
            {
                Email = "newuser@example.com",
                Password = "password123",
                ConfirmPassword = "password123"

            };

            _mockUserManager
                .Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var messageProperty = returnValue?.GetType().GetProperty("Message");
            Assert.NotNull(messageProperty);
            Assert.Equal("Usuário criado com sucesso!", messageProperty.GetValue(returnValue)?.ToString());
        }

        [Fact]
        public async Task Logout_ShouldReturnOk_WhenLogoutIsSuccessful()
        {
            // Arrange
            _mockSignInManager
                .Setup(sm => sm.SignOutAsync())
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Logout();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var messageProperty = returnValue?.GetType().GetProperty("Message");
            Assert.NotNull(messageProperty);
            Assert.Equal("Logout realizado com sucesso.", messageProperty.GetValue(returnValue)?.ToString());
        }
    }
}
