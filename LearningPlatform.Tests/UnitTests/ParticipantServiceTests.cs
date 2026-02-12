using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests;

public class ParticipantServiceTests
{



    //EFTERSOM LÄRARE OCH PARTICIPANT I PRINCIP HAR SAMMA KOD OCH STRUKTUR HAR JAG OMVANDLAT LÄRAREN TILL PARTICIPANT


    // CREATE - undersöker så "spara-knappen" trycks
    [Fact]
    public async Task Create_Saves_To_Database()
    {
        // ARRANGE
        var mockParticipantRepository = new Mock<IParticipantRepository>();                             // Skapar fake test (mock) av Teacher repot
        var mockUnitOfWork = new Mock<IUnitOfWork>();                                                   // Skapar fake test (mock) av UnitOfWork

        var service = new ParticipantService(mockParticipantRepository.Object, mockUnitOfWork.Object);  // Lägger in Teacherrepo och unitofwork i variablen service (Dependency Injection)

        var input = new ParticipantInput(                                                               // "förbereder" test-datan
        FirstName: "Namn",
        LastName: "Efternamn",
        Email: "test@test.org",
        PhoneNumber: "07674635"
        );

        // ACT
        await service.CreateAsync(input);                                                               // 

        // ASSERT
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);      // CHATGPT - denna raden var med hjälp av chatGPT" Raden VARIFIERAR att systemet faktiskt sparar
    }
}
