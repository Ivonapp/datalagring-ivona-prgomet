using LearningPlatform.Application.Enrollments.PersistenceModels;
using LearningPlatform.Infrastructure.EFC.Data;
using LearningPlatform.Infrastructure.EFC.Entities;
using LearningPlatform.Infrastructure.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Tests.Integration;
using Xunit;

namespace LearningPlatform.Tests.Integration.Infrastructure;

[Collection(SqliteInMemoryCollection.Name)]
public sealed class EnrollmentRepositoryTests(SqliteInMemoryFixture fixture)
{

    [Fact]

    //                                      CREATE
    public async Task Add_ShouldWork()
    {

        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        // UNIKA BEROENDEN FÖR ENROLLMENT


        var course = new CourseEntity {                         // COURSE
            Title = "Add", 
            CourseCode = 1, 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);                                 // COURSESESSION
        var session = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);                         // PARTICIPANT
        var part = new ParticipantEntity { 
            FirstName = "A", 
            LastName = "B", 
            Email = "enrollmentcreate@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(part);
        await db.SaveChangesAsync();

        var model = new EnrollmentModel(
            0, 
            new byte[8], 
            DateTime.UtcNow, 
            null, part.Id, 
            session.Id
            );


        //                          Act
        await repo.AddAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        Assert.True(await db.Enrollments.AnyAsync(x => x.ParticipantId == part.Id));
    }




    //                                      READ
    [Fact]
    public async Task Get_ShouldWork()
    {

        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        var course = new CourseEntity { 
            Title = "Get", 
            CourseCode = 2, 
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        var session = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] };

        db.CourseSessions.Add(session);
        var part = new ParticipantEntity { 
            FirstName = "A", 
            LastName = "B", 
            Email = "enrollmentread@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(part);

        var enr = new EnrollmentEntity { 
            Participant = part, 
            CourseSession = session, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enr);
        await db.SaveChangesAsync();

        //                          Act
        var result = await repo.GetByIdAsync(enr.Id);

        //                          Assert
        Assert.NotNull(result);
    }




    //                                      UPDATE
    [Fact]
    public async Task Update_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var repo = new EnrollmentRepository(db);

        var course = new CourseEntity { 
            Title = "Update", 
            CourseCode = 3, 
            Concurrency = new byte[8]
        };

        db.Courses.Add(course);
        var session = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);
        var part = new ParticipantEntity { 
            FirstName = "A", 
            LastName = "B", 
            Email = "enrollmentupdate@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };

        db.Participants.Add(part);

        var enr = new EnrollmentEntity { 
            Participant = part, 
            CourseSession = session, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enr);
        await db.SaveChangesAsync();

        // Skapa en ny session att byta till
        var session2 = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session2);
        await db.SaveChangesAsync();

        //                          Act
        var model = repo.ToModel(enr) with { CourseSessionId = session2.Id };
        await repo.UpdateAsync(model);
        await db.SaveChangesAsync();

        //                          Assert
        var updated = await db.Enrollments.FirstAsync(x => x.Id == enr.Id);
        Assert.Equal(session2.Id, updated.CourseSessionId);
    }




    //                                      DELETE
    [Fact]
    public async Task Delete_ShouldWork()
    {
        //                          Arrange
        await using var db = fixture.CreatedDbContext();
        var course = new CourseEntity { 
            Title = "Del", 
            CourseCode = 4,
            Concurrency = new byte[8] 
        };

        db.Courses.Add(course);
        var session = new CourseSessionEntity { 
            Course = course, 
            StartDate = DateTime.UtcNow, 
            EndDate = DateTime.UtcNow.AddDays(1), 
            Concurrency = new byte[8] 
        };

        db.CourseSessions.Add(session);
        var part = new ParticipantEntity { 
            FirstName = "A", 
            LastName = "B", 
            Email = "eenrollmentdelete@test.com", 
            PhoneNumber = "076123456", 
            Concurrency = new byte[8] 
        };
        db.Participants.Add(part);
        var enr = new EnrollmentEntity {
            Participant = part,
            CourseSession = session, 
            Concurrency = new byte[8] 
        };

        db.Enrollments.Add(enr);
        await db.SaveChangesAsync();

        //                          Act
        db.Enrollments.Remove(enr);
        await db.SaveChangesAsync();

        //                          Assert
        Assert.False(await db.Enrollments.AnyAsync(x => x.Id == enr.Id));
    }
}