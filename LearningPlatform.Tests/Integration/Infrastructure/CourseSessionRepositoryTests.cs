using LearningPlatform.Application.CourseSessions.PersistenceModels;
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
    
    ChatGPT hjälpte mig att SE strukturen och HUR integrationstester skrivs, byggs och bara all-in-all
    hur man ska skriva dom olika delarna. Hur dom olika CRUD och AAA delarna ser ut, Hur man t.ex. ska göra med klasser
    som har olika beroenden såsom Enrollment som har koppling till andra klasser.
    
    Även då denna delen var otroligt svår och jag fick börja om helt, så var det på ett sätt just omstarten som faktiskt
    hjälpte mig att "nöta in" koden.
    
*/


public sealed class CourseSessionRepositoryTests(SqliteInMemoryFixture fixture)
{



    //                      CREATE
    [Fact]
    public async Task Add_ShouldWork()
    {
     //                     Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new CourseSessionRepository(db);

        // BEROEENDET
        var course = new CourseEntity {                 // Course
            Title = "CreateTitle", 
            CourseCode = 201,
            Description = "CreateDescription", 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var model = new CourseSessionModel(0,           //CourseSession
            course.Id, 
            new byte[8], 
            DateTime.UtcNow, 
            DateTime.UtcNow.AddDays(7), 
            DateTime.UtcNow, 
            null);

        //                      Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();

        //                      Assert
        var exists = await db.CourseSessions.AnyAsync(x => x.CourseId == course.Id);
        Assert.True(exists);
    }




    //                          READ
    [Fact]
    public async Task Get_ShouldWork()
    {
        //                      Arrange
        await using var db = fixture.CreatedDbContext();

        var course = new CourseEntity { 
            Title = "ReadTitle", 
            CourseCode = 202, 
            Description = "ReadDescription", 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var coursesession = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(coursesession);
        await db.SaveChangesAsync();

        var repo = new CourseSessionRepository(db);

        //                      Act
        var result = await repo.GetByIdAsync(coursesession.Id);

        //                      Assert
        Assert.NotNull(result);
        Assert.Equal(course.Id, result!.CourseId);
    }





    //                          UPDATE
    //                          
    [Fact]
    public async Task Update_ShouldWork()
    {
        //                      Arrange
        await using var db = fixture.CreatedDbContext();

        var course = new CourseEntity { 
            Title = "UpdateTitle", 
            CourseCode = 203, 
            Description = "UpdateDescription", 
            Concurrency = new byte[8] 
        };



        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var coursesession = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(coursesession);
        await db.SaveChangesAsync();

        var repo = new CourseSessionRepository(db);
        var newDate = DateTime.UtcNow.AddMonths(1);
        var model = repo.ToModel(coursesession) with { EndDate = newDate };




        //                      Act
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //                      Assert
        var updated = await db.CourseSessions.FirstAsync(x => x.Id == coursesession.Id);
        Assert.Equal(newDate.Date, updated.EndDate?.Date);
        Assert.NotNull(updated.UpdatedAt);
    }






    //                          DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        //                      Arrange
        await using var db = fixture.CreatedDbContext();

        var course = new CourseEntity {
            Title = "DeleteTitle", 
            CourseCode = 204, 
            Description = "DeleteDescription", 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var coursesession = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(coursesession);
        await db.SaveChangesAsync();

        //                      Act
        db.CourseSessions.Remove(coursesession);
        await db.SaveChangesAsync();


        //                      Assert
        var exists = await db.CourseSessions.AnyAsync(x => x.Id == coursesession.Id);
        Assert.False(exists);
    }
}