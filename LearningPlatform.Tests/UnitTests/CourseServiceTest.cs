using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Courses.Inputs;
using LearningPlatform.Application.Courses.PersistenceModels;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public sealed class CourseServiceTest
{
    private readonly Mock<ICourseRepository> repo = new();
    private readonly Mock<IUnitOfWork> uow = new();

    private CourseService CreateService()
        => new(repo.Object, uow.Object);


    // CREATE
    [Fact]
    public async Task Create_ShouldRunWithoutErrors()
    {
        var service = CreateService();

        var input = new CourseInput(100, "Title", "Desc", 1);

        repo.Setup(r => r.AddAsync(It.IsAny<CourseModel>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var ex = await Record.ExceptionAsync(() => service.CreateAsync(input));

        Assert.Null(ex);
    }


    // GET
    [Fact]
    public async Task GetById_ShouldReturnCourse()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseModel(1, 100, new byte[] { }, "T", "D", DateTime.UtcNow, null, 1));

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(100, result!.CourseCode);
    }


    // LIST
    [Fact]
    public async Task List_ShouldReturnCourses()
    {
        var service = CreateService();

        repo.Setup(r => r.ListAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<CourseModel>
            {
                new CourseModel(1, 10, new byte[]{}, "A", "B", DateTime.UtcNow, null, 1)
            });

        var list = await service.ListAsync();

        Assert.Single(list);
    }


    // UPDATE
    [Fact]
    public async Task Update_ShouldModifyCourse()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseModel(5, 10, new byte[] { 1 }, "Old", "Desc", DateTime.UtcNow, null, 1));

        var input = new CourseInput(20, "New", "Desc", 1);

        await service.UpdateAsync(5, input);

        repo.Verify(r => r.UpdateAsync(It.Is<CourseModel>(m =>
            m.Id == 5 &&
            m.Title == "New" &&
            m.CourseCode == 20
        ), It.IsAny<CancellationToken>()), Times.Once);

        uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    // DELETE
    [Fact]
    public async Task Delete_ShouldRemoveCourse()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CourseModel(2, 1, new byte[] { }, "T", "D", DateTime.UtcNow, null, 1));

        await service.DeleteAsync(2);

        repo.Verify(r => r.DeleteAsync(2, It.IsAny<CancellationToken>()), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    // DELETE
    [Fact]
    public async Task Delete_ShouldThrow_WhenMissing()
    {
        var service = CreateService();

        repo.Setup(r => r.GetByIdAsync(9, It.IsAny<CancellationToken>()))
            .ReturnsAsync((CourseModel?)null);

        await Assert.ThrowsAsync<ArgumentException>(() => service.DeleteAsync(9));
    }
}
