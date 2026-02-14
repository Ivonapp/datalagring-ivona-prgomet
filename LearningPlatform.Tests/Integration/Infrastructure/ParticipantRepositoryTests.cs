using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Application.Participants.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Integration;
using Xunit;

namespace LearningPlatform.Tests.Integration.Infrastructure;

[Collection(SqliteInMemoryCollection.Name)]




/*                                          *** CHATGPT ***
    
    Jag använde chatgpt som hjälp/bollplank för ALLA klasserna i Integrations tester, men också som ett sätt
    att kunna se och förstå HUR strukturen och koden i integrationstester ser ut i jämförelse med enhetstester. 
    Det tog väldigt lång tid att skriva klart Integrationstesterna (tillskillnad från Enhetstesterna) då enhetsterter
    är mycket enklare att förstå samt mycket enklare kod.
    
    När jag första gången hade skrivit klart alla integrationstester, och försökte köra testet, fick jag buggar och error
    i alla klasser HELA tiden, och hur jag än gjorde, ändrade etc så fortsatte jag få buggar. Tillslut fick jag börja
    om där jag fick radera alla integrationstester jag skrivit och startade om.

Det svåra var första koden fr första klasen (teacher) men när teacher väl funkade, så är participant i prinsip samma 
som teacher koden. Det enda jag ändrade var t.ex

    
    ChatGPT hjälpte mig att SE strukturen och HUR integrationstester skrivs, byggs och bara all-in-all
    hur man ska skriva dom olika delarna. Hur dom olika CRUD och AAA delarna ser ut, Hur man t.ex. ska göra med klasser
    som har olika beroenden såsom Enrollment som har koppling till andra klasser.
    
    Även då denna delen var otroligt svår och jag fick börja om helt, så var det på ett sätt just omstarten som faktiskt
    hjälpte mig att "nöta in" koden.
    
*/

public sealed class ParticipantRepositoryTests(SqliteInMemoryFixture fixture)
{



    //                  EJ CRUD
    // Skapar en participant med den mail vi skickar in med string
    // Här skapat vi en test-person som sen används i alla andra CRUD delar !
    private async Task<ParticipantEntity> TestParticipant(InfrastructureDbContext db, string email)
    {
        var participant = new ParticipantEntity
        {
            FirstName = "TestName",
            LastName = "TestLastName",
            Email = email,
            PhoneNumber = "076123456",
            Concurrency = new byte[8],
            CreatedAt = DateTime.UtcNow
        };

        db.Participants.Add(participant);
        await db.SaveChangesAsync();
        return participant;
    }



    //                  CREATE
    [Fact]
    public async Task AddAsync_ShouldWork()
    {

    //                  Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new ParticipantRepository(db);


        var model = new ParticipantModel(
            0, 
            new byte[8], 
            "FirstName", 
            "Lastname",
            "participantcreate@test.com", 
            "076123456", 
            DateTime.UtcNow, 
            null
            );

    //                  Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();


    //                  Assert
        var exists = await db.Participants.AnyAsync(x => x.Email == "participantcreate@test.com");
        Assert.True(exists);
    }








    //                  READ
    [Fact]
    public async Task GetByIdAsync_ShouldWork()
    {
        //              Arrange
        await using var db = fixture.CreatedDbContext();
        var participant = await TestParticipant(db, "participantread@test.com");                                  //KALLAR PÅ PARTICIPANT
        var repo = new ParticipantRepository(db);

        //              Act
        var result = await repo.GetByIdAsync(participant.Id);

        //              Assert
        Assert.NotNull(result);
        Assert.Equal("participantread@test.com", result!.Email);
    }






    //                  UPDATE
    //                  Här uppdateras 
    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {

        //              Arrange
        await using var db = fixture.CreatedDbContext();
        var participant = await TestParticipant(db, "participantupdate@test.com");                               //KALLAR PÅ PARTICIPANT
        var repo = new ParticipantRepository(db);

        var model = repo.ToModel(participant) with { FirstName = "NyttEsther" };                      // Ändrar namnet till Esther

        //              Act
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //              Assert
        var updated = await db.Participants.FirstAsync(x => x.Id == participant.Id);
        Assert.Equal("NyttEsther", updated.FirstName);                                          // Esther uppdateras till NYA namnet 
        Assert.NotNull(updated.UpdatedAt);                                                      // Säkerställer att det ej är tomt
    }




    //                  DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        //              Arrange
        await using var db = fixture.CreatedDbContext();
        var participant = await TestParticipant(db, "participantdelete@test.com");                                    //KALLAR PÅ PARTICIPANT

        //              Act
        db.Participants.Remove(participant);
        await db.SaveChangesAsync();

        //              Assert
        var exists = await db.Participants.AnyAsync(x => x.Id == participant.Id);
        Assert.False(exists);
    }




    //                  EJ CRUD
    // kollar om en mailadress redan är upptagen innan man försöker skapa en ny användare.

    [Fact]
    public async Task EmailAlreadyExistsAsync_ShouldWork()
    {

        await using var db = fixture.CreatedDbContext();
        await TestParticipant(db, "participantemail_already_exists@test.com");                                      //Lägge in mailen "email_already_exists" i TestParticipant
        var repo = new ParticipantRepository(db);

        var exists = await repo.EmailAlreadyExistsAsync("participantemail_already_exists@test.com");               // Testar om "email_already_exists" redan existerar
        Assert.True(exists);
    }
}



