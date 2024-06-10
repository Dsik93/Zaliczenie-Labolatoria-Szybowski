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
    public class SubjectControllerTests
    {
        private readonly Mock<ISubjectService> _mockSubjectService;
        private readonly SubjectController _controller;

        public SubjectControllerTests()
        {
            _mockSubjectService = new Mock<ISubjectService>();
            _controller = new SubjectController(_mockSubjectService.Object);
        }

        [Fact]
        public async Task GetSubjects_ReturnsOkResult_WithListOfSubjects()
        {
            var subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Math", Description = "Mathematics" },
                new Subject { Id = 2, Name = "Science", Description = "Science" }
            };

            _mockSubjectService.Setup(service => service.GetSubjectsAsync())
                .ReturnsAsync((IEnumerable<Subject>)subjects);

            var result = await _controller.GetSubjects();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnSubjects = Assert.IsType<List<Subject>>(okResult.Value);
            Assert.Equal(2, returnSubjects.Count);
        }

        [Fact]
        public async Task GetSubject_ReturnsNotFound_WhenSubjectDoesNotExist()
        {
            int subjectId = 1;
            _mockSubjectService.Setup(service => service.GetSubjectByIdAsync(subjectId))
                .ReturnsAsync((Subject)null);

            var result = await _controller.GetSubject(subjectId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSubject_ReturnsOkResult_WithSubject()
        {
            int subjectId = 1;
            var subject = new Subject { Id = subjectId, Name = "Math", Description = "Mathematics" };

            _mockSubjectService.Setup(service => service.GetSubjectByIdAsync(subjectId))
                .ReturnsAsync(subject);

            var result = await _controller.GetSubject(subjectId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnSubject = Assert.IsType<Subject>(okResult.Value);
            Assert.Equal(subjectId, returnSubject.Id);
        }

        [Fact]
        public async Task AddSubject_ReturnsCreatedAtActionResult()
        {
            var subject = new Subject { Id = 1, Name = "Math", Description = "Mathematics" };

            _mockSubjectService.Setup(service => service.AddSubjectAsync(subject))
                .Returns(Task.CompletedTask);

            var result = await _controller.AddSubject(subject);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnSubject = Assert.IsType<Subject>(createdAtActionResult.Value);
            Assert.Equal(subject.Id, returnSubject.Id);
        }

        [Fact]
        public async Task UpdateSubject_ReturnsBadRequest_WhenIdMismatch()
        {
            var subject = new Subject { Id = 1, Name = "Math", Description = "Mathematics" };

            var result = await _controller.UpdateSubject(2, subject);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateSubject_ReturnsNoContent()
        {
            var subject = new Subject { Id = 1, Name = "Math", Description = "Mathematics" };

            _mockSubjectService.Setup(service => service.UpdateSubjectAsync(subject))
                .Returns(Task.CompletedTask);

            var result = await _controller.UpdateSubject(1, subject);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteSubject_ReturnsNoContent()
        {
            int subjectId = 1;

            _mockSubjectService.Setup(service => service.DeleteSubjectAsync(subjectId))
                .Returns(Task.CompletedTask);

            var result = await _controller.DeleteSubject(subjectId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}