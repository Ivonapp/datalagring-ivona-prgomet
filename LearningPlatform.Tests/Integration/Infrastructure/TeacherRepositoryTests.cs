using LearningPlatform.Application.Teachers.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Integration;
using Xunit;

namespace LearningPlatform.Tests.Integration.Infrastructure;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class TeacherRepositoryTests(SqliteInMemoryFixture fixture)
{



    //                          EJ CRUD
    // Skapar en participant med den mail vi skickar in med string
    // Här skapat vi en test-person som sen används i CRUD delarna READ, UPDATE, DELETE.
    // Derra då READ, UPDATE och DELETE inte har nån egen data, och för att kunna testa att läsa, ändra eller radera måste det finnas data på plats först.
    // CREATE har redan properties/data så den behöver ej denna
    private async Task<TeacherEntity> TestTeacher(InfrastructureDbContext db, string email)
    {
        var teacher = new TeacherEntity
        {
            FirstName = "TestName",
            LastName = "TestLastName",
            Email = email,                              // Email skrivs unikt varje gång
            PhoneNumber = "076123456",
            Major = "Math",
            Concurrency = new byte[8],
            CreatedAt = DateTime.UtcNow
        };

        db.Teachers.Add(teacher);
        await db.SaveChangesAsync();
        return teacher;
    }





    //                          CREATE
    [Fact]
    public async Task AddAsync_ShouldWork()
    {
        //                      Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new TeacherRepository(db);

        var model = new TeacherModel(
            0,
            "FirstName",
            "LastName",
            "teachercreate@test.com",
            "076123456",
            "Science",
            new byte[8],
            DateTime.UtcNow,
            null
            );

        //                      Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();

        //                      Assert
        var check = await db.Teachers.AnyAsync(x => x.Email == "teachercreate@test.com");
        Assert.True(check);
    }




    //                          READ
    [Fact]
    public async Task GetByIdAsync_ShouldWork()
    {

        //                  Arrange
        await using var db = fixture.CreatedDbContext();
        var teacher = await TestTeacher(db, "teacherread@test.com");                                       // KALLAR PÅ TESTTEACHER
        var repo = new TeacherRepository(db);

        //                  Act
        var result = await repo.GetByIdAsync(teacher.Id);

        //                  Assert
        Assert.NotNull(result);
        Assert.Equal("teacherread@test.com", result!.Email);                                                // SPARAR
    }





    //                          UPDATE
    //                          Här uppdateras endast förnamnet
    [Fact]
    public async Task UpdateAsync_ShouldWork()
    {

        //              Arrange
        await using var db = fixture.CreatedDbContext();
        var teacher = await TestTeacher(db, "teacherupdate@test.com");                                     // KALLAR PÅ TESTTEACHER
        var repo = new TeacherRepository(db);

        var model = repo.ToModel(teacher) with { FirstName = "NyttJohannes" };                      // Johannes blir det NYA namnet 

        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        var updated = await db.Teachers.FirstAsync(x => x.Id == teacher.Id);
        Assert.Equal("NyttJohannes", updated.FirstName);                                            // Johannes uppdateras till NYA namnet 
        Assert.NotNull(updated.UpdatedAt);                                                          // Säkerställer att det ej är tomt
    }






    //                          DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        await using var db = fixture.CreatedDbContext();
        var teacher = await TestTeacher(db, "teacherdelete@test.com");                                     // KALLAR PÅ TESTEACHER

        db.Teachers.Remove(teacher);
        await db.SaveChangesAsync();

        var exists = await db.Teachers.AnyAsync(x => x.Id == teacher.Id);
        Assert.False(exists);

    }







    //                      EJ CRUD
    // kollar om en mailadress redan är upptagen innan man försöker skapa en ny användare.

    [Fact]
    public async Task EmailAlreadyExistsAsync_ShouldWork()
    {

        await using var db = fixture.CreatedDbContext();
        await TestTeacher(db, "teacheremail_already_exists@test.com");                                      //Lägge in mailen "email_already_exists" i TestTeacher
        var repo = new TeacherRepository(db);

        var exists = await repo.EmailAlreadyExistsAsync("teacheremail_already_exists@test.com");            // Testar om "email_already_exists" redan existerar
        Assert.True(exists);
    }
}


