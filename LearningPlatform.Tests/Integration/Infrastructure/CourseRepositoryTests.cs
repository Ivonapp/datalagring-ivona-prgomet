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