using Manero_Backoffice.Business.Models;
using Manero_Backoffice.Data;
using Manero_Backoffice.Services;
using Manero_Backoffice.Tests.Mocks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manero_Backoffice.Tests.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly MockUserManager _userManagerMock;
        private readonly MockSignInManager _signInManagerMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userManagerMock = new MockUserManager();
            _signInManagerMock = new MockSignInManager();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_signInManagerMock, _loggerMock.Object, _userManagerMock);
        }

        [Fact]
        public async Task CreateAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var form = new RegistrationForm
            {
                Email = "test@example.com",
                Password = "Test123!"
            };

            // Act
            var result = await _userService.CreateAsync(form);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var form = new LoginForm
            {
                Email = "test@example.com",
                Password = "Test123!",
                RememberMe = false,
                LockoutOnFailure = false
            };

            // Act
            var result = await _userService.LoginAsync(form);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task AddToRoleAsync_ValidRole_ReturnsSuccess()
        {
            // Arrange
            var user = new ApplicationUser { Email = "test@example.com" };
            var role = "Admin";


            // Act
            var result = await _userService.AddToRoleAsync(user, role);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task FindByEmailAsync_ValidEmail_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";

            // Act
            var result = await _userService.FindByEmailAsync(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }

    }
}
