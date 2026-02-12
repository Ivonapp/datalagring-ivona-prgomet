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
        FirstName: "FirstName",
        LastName: "LastName",
        Email: "Email@test.se",
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









     //                                             UPDATE - Denna kod testar att en uppdatering av en lärare fungerar korrekt genom hela flödet
    [Fact]
    public async Task Update_ShouldUpdateParticipant()
    {
        // ARRANGE
        var mockRepo = new Mock<IParticipantRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new ParticipantService(mockRepo.Object, mockUnitOfWork.Object);

        var existingParticipant = new ParticipantModel(
            1,
            [],
            "FirstName",
            "LastName",
            "Email",
            "Phone",
            DateTime.UtcNow,
            null
        );

        mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))                               // Vi "ställer upp" så att läraren hittas först. SIffran 1 är ID för den läraren som ska ändras
            .ReturnsAsync(existingParticipant);

        // ACT
        await service.UpdateAsync(1, new ParticipantInput(                                                  // är paketet med de nya uppgifterna (namn, mejl, etc.)                                                 
            "NewName",                                                                                      // som ska skriva över dom gamla upgifterna
            "NewLastName",
            "Email@test.se",
            "07658236"));

        // ASSERT
        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ParticipantModel>()), Times.Once);                      // Verifierar att Update-metoden i repot anropades
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);          // Verifierar att spara-knappen trycktes
        }
    }

     
    
