using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests;

public class CourseSessionServiceTest
{

    //                     CREATE - skapar CourseSession
    [Fact]
    public async Task Should_Create_And_Save_CourseSession()
    {
        // ARRANGE
        var mockCourseSessionRepository = new Mock<ICourseSessionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseSessionService(mockCourseSessionRepository.Object, mockUnitOfWork.Object);

        var input = new CourseSessionInput(
        1,
        DateTime.Now,
        null
        );

        // ACT
        await service.CreateAsync(input);

        // ASSERT
        mockCourseSessionRepository.Verify(r => r.AddAsync(It.IsAny<CourseSessionModel>(), It.IsAny<CancellationToken>()), Times.Once);     // Kollar att AddAsync försökte lägga till en kurs-session i repositoryt
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);                                          // Ändring sparas
    }






// READ (GetById)
    [Fact]
    public async Task GetById_ShouldReturnSession_WhenExists()
    {
        // ARRANGE
        var mockCourseSessionRepository = new Mock<ICourseSessionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseSessionService(mockCourseSessionRepository.Object, mockUnitOfWork.Object);


        var existing = new CourseSessionModel(
            1,
            10,
            [],
            DateTime.Now,
            null,
            DateTime.UtcNow,
            null
            );

        mockCourseSessionRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        // ACT
        var result = await service.GetByIdAsync(1);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }






    // UPDATE
    [Fact]
    public async Task Update_ShouldModifySession_WhenExists()
    {
        // ARRANGE
        var mockCourseSessionRepository = new Mock<ICourseSessionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseSessionService(mockCourseSessionRepository.Object, mockUnitOfWork.Object);

        var existing = new CourseSessionModel(
            1,
            10,
            [],
            DateTime.Now,
            null,
            DateTime.UtcNow,
            null
            );

        mockCourseSessionRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        var input = new CourseSessionInput(
            10,
            DateTime.Now.AddDays(1),
            null
            );

        // ACT
        await service.UpdateAsync(1, input);

        // ASSERT
        mockCourseSessionRepository.Verify(r => r.UpdateAsync(It.IsAny<CourseSessionModel>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }







    // DELETE
    [Fact]
    public async Task Delete_ShouldRemoveSession_WhenExists()
    {
        // ARRANGE
        var mockCourseSessionRepository = new Mock<ICourseSessionRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseSessionService(mockCourseSessionRepository.Object, mockUnitOfWork.Object);

        var existing = new CourseSessionModel(
            1,
            10,
            [],
            DateTime.Now,
            null,
            DateTime.UtcNow,
            null
            );

        mockCourseSessionRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        // ACT
        await service.DeleteAsync(1);

        // ASSERT
        mockCourseSessionRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}