using LearningPlatform.Application.Abstractions.Persistence;
using LearningPlatform.Application.Abstractions.Persistence.Repositories;
using LearningPlatform.Application.Teachers;
using LearningPlatform.Application.Teachers.Inputs;
using LearningPlatform.Application.Teachers.Outputs;
using LearningPlatform.Application.Teachers.PersistenceModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;


// CHATGPT - Fick hjälp av chatgpt med hur jag skulle strukturera upp denna delen med enhetstester då det kan bli rätt rörigt och förvirrande snabbt.
// Jag valde att hålla det relativt enkelt och fokuserade mer på att förstå vad dom olika delarna (Arrange, Act, Assert) faktiskt gör.

namespace LearningPlatform.Tests.UnitTests;

public class TeacherServiceTests
{



    //                                         CREATE - undersöker så "spara-knappen" trycks
    [Fact]
    public async Task Create_Saves_To_Database()
    {
        // ARRANGE
        var mockTeacherRepository = new Mock<ITeacherRepository>();                                     // Skapar fake test (mock) av Teacher repot
        var mockUnitOfWork = new Mock<IUnitOfWork>();                                                   // Skapar fake test (mock) av UnitOfWork

        var service = new TeacherService(mockTeacherRepository.Object, mockUnitOfWork.Object);          // Lägger in Teacherrepo och unitofwork i variablen service (Dependency Injection)

        var input = new TeacherInput(                                                                   // "förbereder" test-datan
         FirstName: "FirstName",
         LastName: "LastName",
         Email: "Email@test.se",
         PhoneNumber: "07674635",
         Major: "Math"
        );

        // ACT
        await service.CreateAsync(input);

        // ASSERT
        mockUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);      // CHATGPT - denna raden var med hjälp av chatGPT" Raden VARIFIERAR att systemet faktiskt sparar
    }








    //                                          READ - TestAR att en lärare hittas i databasen VIA ID (GetByIdAsync)
    [Fact]
    public async Task GetById_ReturnsTeacher_WhenExists()
    {

        // ARRANGE
        var mockRepo = new Mock<ITeacherRepository>();                                          // Skapar en fake (mock) repository                    
        var mockUow = new Mock<IUnitOfWork>();                                                  // Skapar en fake (mock) UnitOfWork  
        var service = new TeacherService(mockRepo.Object, mockUow.Object);                      // Skapar service och injicerar mockRepo och mockUow istället för riktiga beroenden

        var fakeTeacherModel = new TeacherModel(                                                // Skapar en fake lärare (testdata)
            1,
            "",                                                                                 // Fyllde inte i Firstname, LastName, Email eyc eftersom vi ENDAST söker på ID.
            "",                                                                                 // Ville därav göra det tydligare och fila bort all onödig info. 
            "",
            "",
            "",
            [],
            DateTime.UtcNow,
            null
        );

        mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))                   // Fick hjälp av ChatGpt för denna delen då jag inte fick rätt på det. 
                .ReturnsAsync(fakeTeacherModel);                                                // Returnera "fakeTeacherModel" när GetByIdAsync anropas

        // ACT
        var result = await service.GetByIdAsync(1);                                             // Anropar metoden vi vill testa

        // ASSERT
        Assert.NotNull(result);                                                                 // Kontrollerar att resultatet inte är null (dvs att en lärare hittdes)
    }










    //                                             UPDATE - Denna kod testar att en uppdatering av en lärare fungerar korrekt genom hela flödet
    [Fact]
    public async Task Update_ShouldUpdateTeacher()
    {
        // ARRANGE
        var mockRepo = new Mock<ITeacherRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var service = new TeacherService(mockRepo.Object, mockUnitOfWork.Object);

        var existingTeacher = new TeacherModel(
            1,
            "FirstName",
            "LastName",
            "Email",
            "Phone",
            "Major",
            [],
            DateTime.UtcNow,
            null
        );

        mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))                               // Vi "ställer upp" så att läraren hittas först. SIffran 1 är ID för den läraren som ska ändras
            .ReturnsAsync(existingTeacher);

        // ACT
        await service.UpdateAsync(1, new TeacherInput(                                                      // är paketet med de nya uppgifterna (namn, mejl, etc.)                                                 
            "NewName",                                                                                      // som ska skriva över dom gamla upgifterna
            "NewLastName",
            "Email@test.se",
            "07658236",
            "Math"));

        // ASSERT
        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<TeacherModel>()), Times.Once);                          // Verifierar att Update-metoden i repot anropades
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);          // Verifierar att spara-knappen trycktes
        }
    }
