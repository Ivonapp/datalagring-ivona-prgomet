using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.CourseSessions;
using LearningPlatform.Application.CourseSessions.Inputs;
using LearningPlatform.Application.CourseSessions.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests
{
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
    }
}
 