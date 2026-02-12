using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Participants;
using LearningPlatform.Application.Participants.Inputs;
using LearningPlatform.Application.Participants.Outputs;
using LearningPlatform.Application.Participants.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningPlatform.Tests.UnitTests;

public class ParticipantServiceTests
{



    //EFTERSOM LÄRARE OCH PARTICIPANT I PRINCIP HAR SAMMA KOD OCH STRUKTUR HAR JAG OMVANDLAT LÄRAREN TILL PARTICIPANT


    //                                                              CREATE - undersöker så "spara-knappen" trycks
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







     //                                          READ - TestAR att en participand/deltagare hittas i databasen VIA ID (GetByIdAsync)
    [Fact]
    public async Task GetById_ReturnsParticipant_WhenExists()
    {

        // ARRANGE
        var mockRepo = new Mock<IParticipantRepository>();                                      // Skapar en fake (mock) repository                    
        var mockUow = new Mock<IUnitOfWork>();                                                  // Skapar en fake (mock) UnitOfWork  
        var service = new ParticipantService(mockRepo.Object, mockUow.Object);                  // Skapar service och injicerar mockRepo och mockUow istället för riktiga beroenden

        var fakeParticipantModel = new ParticipantModel(                                        // Skapar en fake lärare (testdata)
            1,
            [],
            "",                                                                                 // Fyllde inte i Firstname, LastName, Email eyc eftersom vi ENDAST söker på ID.
            "",                                                                                 // Ville därav göra det tydligare och fila bort all onödig info. 
            "",
            "",
            DateTime.UtcNow,
            null
        );

        mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))                   // Fick hjälp av ChatGpt för denna delen då jag inte fick rätt på det. 
                .ReturnsAsync(fakeParticipantModel);                                            // Returnera "fakeTeacherModel" när GetByIdAsync anropas

        // ACT
        var result = await service.GetByIdAsync(1);                                             // Anropar metoden vi vill testa

        // ASSERT
        Assert.NotNull(result);                                                                 // Kontrollerar att resultatet inte är null (dvs att en lärare hittdes)
    }
}



