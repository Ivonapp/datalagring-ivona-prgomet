using LearningPlatform.Application.CourseSessions.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Integration;
using Xunit;

namespace LearningPlatform.Tests.Integration.Infrastructure;

[Collection(SqliteInMemoryCollection.Name)]
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
            Title = "AddSession", 
            CourseCode = 10,
            Description = "Test", 
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
            Title = "GetSession", 
            CourseCode = 20, 
            Description = "Test", 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var session = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);
        await db.SaveChangesAsync();

        var repo = new CourseSessionRepository(db);

        //                      Act
        var result = await repo.GetByIdAsync(session.Id);

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
            Title = "UpdateSession", 
            CourseCode = 30, 
            Description = "Test", 
            Concurrency = new byte[8] 
        };



        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var session = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);
        await db.SaveChangesAsync();

        var repo = new CourseSessionRepository(db);
        var newDate = DateTime.UtcNow.AddMonths(1);
        var model = repo.ToModel(session) with { EndDate = newDate };




        //                      Act
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //                      Assert
        var updated = await db.CourseSessions.FirstAsync(x => x.Id == session.Id);
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
            Title = "DeleteSession", 
            CourseCode = 40, 
            Description = "Test", 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var session = new CourseSessionEntity { 
            CourseId = course.Id, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(7), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);
        await db.SaveChangesAsync();

        //                      Act
        db.CourseSessions.Remove(session);
        await db.SaveChangesAsync();


        //                      Assert
        var exists = await db.CourseSessions.AnyAsync(x => x.Id == session.Id);
        Assert.False(exists);
    }
}