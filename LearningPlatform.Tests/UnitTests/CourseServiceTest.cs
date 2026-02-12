using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Courses;
using LearningPlatform.Application.Courses.Inputs;
using Moq;
using LearningPlatform.Application.Courses.PersistenceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests;

public class CourseServiceTest
{



//                                              CREATE - Skapar kursen och sparar den
    [Fact]
    public async Task Create_ShouldAddCourse_AndSave()
    {
        // ARRANGE
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

        var input = new CourseInput(
            101,
            "Mathematics",
            "Mathematics A"
            );

        // ACT
        await service.CreateAsync(input);

        // ASSERT
        mockCourseRepository.Verify(r => r.AddAsync(It.IsAny<CourseModel>(), It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }








//                                          READ - Hämtar kurs via ID
    [Fact]
    public async Task GetById_ShouldReturnCourse_WhenExists()
    {

        // ARRANGE
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

        var existing = new CourseModel(
            1,
            101,
            [],
            "Title",
            "Desc",
            DateTime.UtcNow,
            null
            );

        mockCourseRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        // ACT
        var result = await service.GetByIdAsync(1);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal("Title", result.Title);
    }








    //                                          UPDATE - Uppdaterar kursen
    [Fact]
    public async Task Update_ShouldModifyCourse_WhenExists()
    {
        // ARRANGE
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

        var existing = new CourseModel(
            1,
            101,
            [],
            "Old Title",
            "Old Description",
            DateTime.UtcNow,
            null
            );

        mockCourseRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        var input = new CourseInput(
            101,
            "New Title",
            "New Description"
            );

        // ACT
        await service.UpdateAsync(1, input);

        // ASSERT
        mockCourseRepository.Verify(r => r.UpdateAsync(It.Is<CourseModel>(c => c.Title == "New Title"), It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }








    //                                          DELETE - Raderar kursen
    [Fact]
    public async Task Delete_ShouldRemoveCourse_WhenExists()
    {
        // ARRANGE
        var mockCourseRepository = new Mock<ICourseRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object);

        var existing = new CourseModel(                                                             // Skapar fake kursen som ska raderas
            1,
            101,
            [],
            "",
            "",
            DateTime.UtcNow,
            null
            );

        mockCourseRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

        // ACT
        await service.DeleteAsync(1);

        // ASSERT
        mockCourseRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);                  // Sparar
    }
}
