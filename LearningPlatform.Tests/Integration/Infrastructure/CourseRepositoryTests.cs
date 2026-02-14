using LearningPlatform.Application.Courses.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
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
    
    ChatGPT hjälpte mig att SE strukturen och HUR integrationstester skrivs, byggs och bara all-in-all
    hur man ska skriva dom olika delarna. Hur dom olika CRUD och AAA delarna ser ut, Hur man t.ex. ska göra med klasser
    som har olika beroenden såsom Enrollment som har koppling till andra klasser.
    
    Även då denna delen var otroligt svår och jag fick börja om helt, så var det på ett sätt just omstarten som faktiskt
    hjälpte mig att "nöta in" koden.
    
*/

public sealed class CourseRepositoryTests(SqliteInMemoryFixture fixture)
{

    //                              CREATE
    [Fact]
    public async Task Add_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new CourseRepository(db);

        var model = new CourseModel(
            0,
            301,
            new byte[8],
            "CreateTitle",
            "CreateDescription",
            DateTime.UtcNow,
            null
            );

        //                          Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        var exists = await db.Courses.AnyAsync(x => x.CourseCode == 301);
        Assert.True(exists);
    }




    //                              READ
    [Fact]
    public async Task Get_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new CourseRepository(db);

        var course = new CourseEntity { 
            Title = "ReadTitle",
            CourseCode = 302,
            Description = "ReadDescription",
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        //                          Act
        var result = await repo.GetByIdAsync(course.Id);

        //                          Assert
        Assert.NotNull(result);
        Assert.Equal(302, result!.CourseCode);
    }




    //                              UPDATE
    //                              Ändrar titel endast
    [Fact]
    public async Task Update_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new CourseRepository(db);

        var course = new CourseEntity {
            Title = "OldTitle",
            CourseCode = 303,
            Description = "Description",
            Concurrency = new byte[8]
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var model = repo.ToModel(course) with { Title = "NewTitle" };                   // ÄNDRAR TITEL

        //                          Act
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        var updated = await db.Courses.FirstAsync(x => x.Id == course.Id);
        Assert.Equal("NewTitle", updated.Title);                                        // SPARAR TITEL
        Assert.NotNull(updated.UpdatedAt);
    }




    //                              DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();

        var course = new CourseEntity {
            Title = "DeleteTitle",
            CourseCode = 304,
            Description = "DeleteDescription",
            Concurrency = new byte[8]
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        //                          Act
        db.Courses.Remove(course);
        await db.SaveChangesAsync();

        //                          Assert
        var exists = await db.Courses.AnyAsync(x => x.Id == course.Id);
        Assert.False(exists);
    }
}