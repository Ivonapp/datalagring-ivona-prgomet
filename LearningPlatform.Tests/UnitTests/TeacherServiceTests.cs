using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Application.Teachers.Inputs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


// CHATGPT - Fick hjälp av chatgpt med hur jag skulle strukturera upp denna delen med enhetstester då det kan bli rätt rörigt och förvirrande snabbt.
// Jag valde att hålla det relativt enkelt och fokuserade mer på att förstå vad dom olika delarna (Arrange, Act, Assert) faktiskt gör.

namespace LearningPlatform.Tests.UnitTests;

public class TeacherServiceTests
{



    // CREATE - undersöker så "spara-knappen" trycks
    [Fact]
    public async Task Create_Saves_To_Database()
    {
        // ARRANGE
        var mockTeacherRepository = new Mock<ITeacherRepository>();                                     // Skapar fake test (mock) av Teacher repot
        var mockUnitOfWork = new Mock<IUnitOfWork>();                                                   // Skapar fake test (mock) av UnitOfWork

        var service = new TeacherService(mockTeacherRepository.Object, mockUnitOfWork.Object);          // Lägger in Teacherrepo och unitofwork i variablen service (Dependency Injection)

        var input = new TeacherInput(                                                                   // "förbereder" test-datan
         FirstName: "Namn",
         LastName: "Efternamn",
         Email: "test@test.org",
         PhoneNumber: "07674635",
         Major: "Fysik"
        );

        // ACT
        await service.CreateAsync(input);

        // ASSERT
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);      // CHATGPT - denna raden var med hjälp av chatGPT" Raden VARIFIERAR att systemet faktiskt sparar
    }
}
