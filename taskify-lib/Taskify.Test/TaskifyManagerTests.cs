namespace Taskify.Test
{
    using Moq;
    using NUnit.Framework;
    using Taskify.Data.Models;
    using Taskify.Data.Repositories;
    using TaskifyAPI.Dtos;
    using TaskifyAPI.Managers;

    [TestFixture]
    public class TaskifyManagerTests
    {
        private Mock<ITaskRepository> _mockTaskRepository;
        private TaskifyManager _manager;

        [SetUp]
        public void Setup()
        {
            _mockTaskRepository = new Mock<ITaskRepository>();
            _manager = new TaskifyManager(_mockTaskRepository.Object);
        }

        [Test]
        public async Task CreateNewTaskAsync_Should_Create_New_Task()
        {
            // Arrange
            var dto = new CreateNewTaskDto(
                "Test Title",
                "Test Description",
                Guid.NewGuid())
            {
                CreatedBy = "Test User",
                CreationDate = DateTime.Now
            };

            _mockTaskRepository.Setup(m => m.UpsertAsync(It.IsAny<TaskModel>()))
            .ReturnsAsync((TaskModel tm) => tm);

            // Act
            var result = await _manager.CreateNewTaskAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            Assert.That(result.Title, Is.EqualTo(dto.Title));
            Assert.That(result.ParentTask, Is.EqualTo(dto.ParentId));
            Assert.That(result.CreatedBy, Is.EqualTo(dto.CreatedBy));
            Assert.That(result.CreationDate, Is.EqualTo(dto.CreationDate));
        }

        [Test]
        public async Task Fetch_Should_Return_Task()
        {
            // Arrange
            var key = new TaskKey(Guid.NewGuid(), Guid.NewGuid());
            var task = new TaskModel
            {
                Id = key.Id,
                ParentTask = key.ParentId
            };
            _mockTaskRepository
                .Setup(m => m.GetAsync(It.Is<Guid>((id) => id == task.Id), It.Is<Guid?>((pid) => pid == task.ParentTask)))
                .ReturnsAsync(() => task);

            // Act
            var result = await _manager.Fetch(key);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(key.Id));
            Assert.That(result.ParentTask, Is.EqualTo(key.ParentId));
        }

        [Test]
        public async Task Update_Should_Update_Task()
        {
            // Arrange
            var taskKey = new TaskKey(Guid.NewGuid(), Guid.NewGuid());
            var dto = new UpdateTaskDto(
                taskKey,
                "Test Title Modified",
                "Test Description Modified");

            _mockTaskRepository.Setup(m => m.GetAsync(It.Is<Guid>(g => g == taskKey.Id), It.IsAny<Guid>()))
                .ReturnsAsync(() => new TaskModel
                {
                    Id = taskKey.Id,
                    ParentTask = taskKey.ParentId,
                    Title = "Test Title",
                    Description = "Test Title"
                });
            _mockTaskRepository.Setup(m => m.UpsertAsync(It.IsAny<TaskModel>()))
            .ReturnsAsync((TaskModel tm) => tm);

            // Act
            var result = await _manager.UpdateTaskAsync(dto);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Description, Is.EqualTo(dto.Description));
            Assert.That(result.Title, Is.EqualTo(dto.Title));
        }    }
}