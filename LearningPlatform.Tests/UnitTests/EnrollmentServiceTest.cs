using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Enrollments;
using LearningPlatform.Application.Enrollments.Inputs;
using LearningPlatform.Application.Enrollments.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests;


    public class EnrollmentServiceTests
    {



        //                                              CREATE - skapar och sprar enrollment
        [Fact]
        public async Task Create_ShouldAddEnrollment_AndSave()
        {
            var mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new EnrollmentService(mockEnrollmentRepository.Object, mockUnitOfWork.Object);

            var input = new EnrollmentInput(
                1,
                1
                );                                                              //ID för både participant och courseSession

            await service.CreateAsync(input);

            mockEnrollmentRepository.Verify(r => r.AddAsync(It.IsAny<EnrollmentModel>(), It.IsAny<CancellationToken>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }




        //                                              READ - Hämtar Enrollment
        [Fact]
        public async Task GetById_ShouldReturnEnrollment_WhenExists()
        {
            var mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new EnrollmentService(mockEnrollmentRepository.Object, mockUnitOfWork.Object);

            var existing = new EnrollmentModel(
                1,
                [],
                DateTime.UtcNow,
                null,
                1,
                1
                );
            mockEnrollmentRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

            var result = await service.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.ParticipantId);
            Assert.Equal(1, result.CourseSessionId);
        }




    //                                              UPDATE - kontrollerar ändring
    [Fact]
        public async Task Update_ShouldUpdateEnrollment_WhenExists()
        {
            var mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new EnrollmentService(mockEnrollmentRepository.Object, mockUnitOfWork.Object);

            var existing = new EnrollmentModel(
                1,
                [],
                DateTime.UtcNow,
                null,
                1,
                1
                );
            mockEnrollmentRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

            var input = new EnrollmentInput(2, 3);                                                                    // Ny participant och courseSession

            await service.UpdateAsync(1, input);

            mockEnrollmentRepository.Verify(r => r.UpdateAsync(It.Is<EnrollmentModel>(e => e.ParticipantId == 2 && e.CourseSessionId == 3), It.IsAny<CancellationToken>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }




        //                                              DELETE - raderar enrollmen
        [Fact]
        public async Task Delete_ShouldRemoveEnrollment_WhenExists()
        {
            var mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var service = new EnrollmentService(mockEnrollmentRepository.Object, mockUnitOfWork.Object);

            var existing = new EnrollmentModel(
                1,
                [],
                DateTime.UtcNow,
                null,
                1,
                1
                );

            mockEnrollmentRepository.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

            await service.DeleteAsync(1);

            mockEnrollmentRepository.Verify(r => r.DeleteAsync(1, It.IsAny<CancellationToken>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }








 