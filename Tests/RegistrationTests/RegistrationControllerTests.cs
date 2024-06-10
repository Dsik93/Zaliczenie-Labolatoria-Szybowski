using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Model;
using WebApi.Service;
using Xunit;

namespace WebApi.Tests
{
    public class RegistrationControllerTests
    {
        private readonly Mock<IRegistrationService> _mockRegistrationService;
        private readonly RegistrationController _controller;

        public RegistrationControllerTests()
        {
            _mockRegistrationService = new Mock<IRegistrationService>();
            _controller = new RegistrationController(_mockRegistrationService.Object);
        }

        [Fact]
        public async Task GetRegistrations_ReturnsOkResult_WithListOfRegistrations()
        {
            var registrations = new List<Registration>
            {
                new Registration { Id = 1, StudentId = 1, SubjectId = 1, RegistrationDate = DateTime.Now },
                new Registration { Id = 2, StudentId = 2, SubjectId = 2, RegistrationDate = DateTime.Now }
            };

            _mockRegistrationService.Setup(service => service.GetRegistrationsAsync())
                .ReturnsAsync((IEnumerable<Registration>)registrations);

            var result = await _controller.GetRegistrations();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnRegistrations = Assert.IsType<List<Registration>>(okResult.Value);
            Assert.Equal(2, returnRegistrations.Count);
        }

        [Fact]
        public async Task GetRegistration_ReturnsNotFound_WhenRegistrationDoesNotExist()
        {
            int registrationId = 1;
            _mockRegistrationService.Setup(service => service.GetRegistrationByIdAsync(registrationId))
                .ReturnsAsync((Registration)null);

            var result = await _controller.GetRegistration(registrationId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetRegistration_ReturnsOkResult_WithRegistration()
        {
            int registrationId = 1;
            var registration = new Registration { Id = registrationId, StudentId = 1, SubjectId = 1, RegistrationDate = DateTime.Now };

            _mockRegistrationService.Setup(service => service.GetRegistrationByIdAsync(registrationId))
                .ReturnsAsync(registration);

            var result = await _controller.GetRegistration(registrationId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnRegistration = Assert.IsType<Registration>(okResult.Value);
            Assert.Equal(registrationId, returnRegistration.Id);
        }

        [Fact]
        public async Task AddRegistration_ReturnsCreatedAtActionResult()
        {
            var registration = new Registration { Id = 1, StudentId = 1, SubjectId = 1, RegistrationDate = DateTime.Now };

            _mockRegistrationService.Setup(service => service.AddRegistrationAsync(registration))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddRegistration(registration);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnRegistration = Assert.IsType<Registration>(createdAtActionResult.Value);
            Assert.Equal(registration.Id, returnRegistration.Id);
        }

        [Fact]
        public async Task UpdateRegistration_ReturnsBadRequest_WhenIdMismatch()
        {
            var registration = new Registration { Id = 1, StudentId = 1, SubjectId = 1, RegistrationDate = DateTime.Now };

            var result = await _controller.UpdateRegistration(2, registration);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateRegistration_ReturnsNoContent()
        {
            var registration = new Registration { Id = 1, StudentId = 1, SubjectId = 1, RegistrationDate = DateTime.Now };

            _mockRegistrationService.Setup(service => service.UpdateRegistrationAsync(registration))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateRegistration(1, registration);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteRegistration_ReturnsNoContent()
        {
            int registrationId = 1;

            _mockRegistrationService.Setup(service => service.DeleteRegistrationAsync(registrationId))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteRegistration(registrationId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
