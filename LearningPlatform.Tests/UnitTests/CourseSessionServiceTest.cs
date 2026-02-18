using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using LearningPlatform.Application.CourseSessions.Outputs;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public sealed class CourseSessionServiceTest
{
    private readonly Mock<ICourseSessionRepository> repo = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private CourseSessionService CreateService()
        => new(repo.Object, uow.Object);

    // CREATE
    [Fact]
    public async Task Create_ShouldRunWithoutErrors()
    {
        var service = CreateService();

        var input = new CourseSessionInput(1, DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

        repo.Setup(r => r.AddAsync(It.IsAny<CourseSessionModel>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var ex = await Record.ExceptionAsync(() => service.CreateAsync(input));

        Assert.Null(ex);
    }

    // READ
    [Fact]
    public async Task Read_ShouldReturnCourseSession()
    {
        var service = CreateService();

        var now = DateTime.UtcNow;

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseSessionModel(1, 1, Array.Empty<byte>(), now, now.AddHours(2), now, null));

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.CourseId);
        Assert.Equal(now, result.StartDate);
    }

    // READ 
    [Fact]
    public async Task List_ShouldReturnCourseSessions()
    {
        var service = CreateService();

        var now = DateTime.UtcNow;

        repo.Setup(r => r.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CourseSessionModel>
            {
                new CourseSessionModel(1, 1, Array.Empty<byte>(), now, now.AddHours(2), now, null)
            });

        var list = await service.ListAsync();

        Assert.Single(list);
    }

    // UPDATE
    [Fact]
    public async Task Update_ShouldCallRepository()
    {
        var service = CreateService();

        var now = DateTime.UtcNow;

        // Repo returnerar "existing" model
        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseSessionModel(1, 1, Array.Empty<byte>(), now, now.AddHours(2), now, null));

        var input = new CourseSessionInput(2, now.AddDays(1), now.AddDays(2));

        await service.UpdateAsync(1, input);

        repo.Verify(r => r.UpdateAsync(It.Is<CourseSessionModel>(m =>
            m.Id == 1 &&
            m.CourseId == 2 &&
            m.StartDate == now.AddDays(1) &&
            m.EndDate == now.AddDays(2)
        ), It.IsAny<CancellationToken>()), Times.Once);

        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // DELETE
    [Fact]
    public async Task Delete_ShouldCallRepository()
    {
        var service = CreateService();

        var now = DateTime.UtcNow;

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseSessionModel(1, 1, Array.Empty<byte>(), now, now.AddHours(2), now, null));

        await service.DeleteAsync(1);

        repo.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    // DELETE 
    [Fact]
    public async Task Delete_ShouldThrow_WhenMissing()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CourseSessionModel?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(999));
    }
}
