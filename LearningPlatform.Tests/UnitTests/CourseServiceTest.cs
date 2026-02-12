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
}


