using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Model;
using WebApi.Service;
using Xunit;

namespace WebApi.Tests
{
    public class TeacherControllerTests
    {
        private readonly Mock<ITeacherService> _mockTeacherService;
        private readonly TeacherController _controller;

        public TeacherControllerTests()
        {
            _mockTeacherService = new Mock<ITeacherService>();
            _controller = new TeacherController(_mockTeacherService.Object);
        }

        [Fact]
        public async Task GetTeachers_ReturnsOkResult_WithListOfTeachers()
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Teacher { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" }
            };

            _mockTeacherService.Setup(service => service.GetTeachersAsync())
                .ReturnsAsync(teachers);

            var result = await _controller.GetTeachers();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTeachers = Assert.IsType<List<Teacher>>(okResult.Value);
            Assert.Equal(2, returnTeachers.Count);
        }

        [Fact]
        public async Task GetTeacher_ReturnsNotFound_WhenTeacherDoesNotExist()
        {
            int teacherId = 1;
            _mockTeacherService.Setup(service => service.GetTeacherByIdAsync(teacherId))
                .ReturnsAsync((Teacher)null);

            var result = await _controller.GetTeacher(teacherId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetTeacher_ReturnsOkResult_WithTeacher()
        {
            int teacherId = 1;
            var teacher = new Teacher { Id = teacherId, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            _mockTeacherService.Setup(service => service.GetTeacherByIdAsync(teacherId))
                .ReturnsAsync(teacher);

            var result = await _controller.GetTeacher(teacherId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTeacher = Assert.IsType<Teacher>(okResult.Value);
            Assert.Equal(teacherId, returnTeacher.Id);
        }

        [Fact]
        public async Task AddTeacher_ReturnsCreatedAtActionResult()
        {
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            _mockTeacherService.Setup(service => service.AddTeacherAsync(teacher))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddTeacher(teacher);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnTeacher = Assert.IsType<Teacher>(createdAtActionResult.Value);
            Assert.Equal(teacher.Id, returnTeacher.Id);
        }

        [Fact]
        public async Task UpdateTeacher_ReturnsBadRequest_WhenIdMismatch()
        {
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            var result = await _controller.UpdateTeacher(2, teacher);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateTeacher_ReturnsNoContent()
        {
            var teacher = new Teacher { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            _mockTeacherService.Setup(service => service.UpdateTeacherAsync(teacher))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateTeacher(1, teacher);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteTeacher_ReturnsNoContent()
        {
            int teacherId = 1;

            _mockTeacherService.Setup(service => service.DeleteTeacherAsync(teacherId))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteTeacher(teacherId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
